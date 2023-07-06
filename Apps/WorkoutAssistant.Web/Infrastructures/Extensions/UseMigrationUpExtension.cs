using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WorkoutAssistant.Web.Infrastructures.Contracts;

namespace WorkoutAssistant.Web.Infrastructures.Extensions;

public static class UseMigrationUpExtension
{
    public static async void UseMigrationUp(this IApplicationBuilder entry)
    {
        using (IServiceScope scope = entry.ApplicationServices.CreateScope())
        {
            IMigratorService?
                service = scope.ServiceProvider.GetService<IMigratorService>();

            if (service != null)
            {
                await service.MigrateUpAsync();
            }
        }
        
        //return entry;
    }
}