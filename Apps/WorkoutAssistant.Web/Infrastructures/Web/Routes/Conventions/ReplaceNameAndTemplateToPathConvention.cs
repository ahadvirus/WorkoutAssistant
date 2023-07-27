using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using WorkoutAssistant.Web.Infrastructures.Extensions;

namespace WorkoutAssistant.Web.Infrastructures.Web.Routes.Conventions;

public class ReplaceNameAndTemplateToPathConvention : IApplicationModelConvention
{
    private void UpdateSelectors(IEnumerable<SelectorModel> selectors)
    {
        foreach (SelectorModel selector in selectors.Where(predicate: selector => selector.AttributeRouteModel != null))
        {
            if (selector.AttributeRouteModel != null)
            {
                AttributeRouteModel? routeModel = AttributeRouteModel.CombineAttributeRouteModel(
                    left: selector.AttributeRouteModel,
                    right: new AttributeRouteModel()
                );

                if (routeModel != null)
                {
                    string? name = !string.IsNullOrEmpty(value: routeModel.Name) ? routeModel.Name : routeModel.Template;
                    if (!string.IsNullOrEmpty(value: name))
                    {
                        routeModel.Template = name.GetRoutePathByName();
                    }
                }

                selector.AttributeRouteModel = routeModel;
            }
        }
    }

    public void Apply(ApplicationModel application)
    {
        foreach (ActionModel action in application.Controllers.SelectMany(selector: controller => controller.Actions))
        {
            UpdateSelectors(selectors: action.Selectors);
        }
    }
}