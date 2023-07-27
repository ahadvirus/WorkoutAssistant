using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Mediator;
using Newtonsoft.Json;
using WorkoutAssistant.Web.Areas.Auth.Models.DataTransfers;
using WorkoutAssistant.Web.Areas.Auth.Models.ViewModels;

namespace WorkoutAssistant.Web.Areas.Auth.Applications;

public record LoginHandler : IRequestHandler<LoginVm, UserLoginInfoDto>
{
    public ValueTask<UserLoginInfoDto> Handle(LoginVm request, CancellationToken cancellationToken)
    {
        UserLoginInfoDto result;
        
        ICollection<ValidationResult> collection = new List<ValidationResult>();
        
        if (Validator.TryValidateObject(instance: request,
                validationContext: new ValidationContext(instance: request),
                validationResults: collection,
                validateAllProperties: true))
        {
            result = new UserLoginInfoDto();
        }
        else
        {
            throw new InvalidDataException(
                message: JsonConvert.SerializeObject(value: collection, formatting: Formatting.None));
        }

        return ValueTask.FromResult(result: result);
    }
}