using Microsoft.AspNetCore.Builder;

namespace WorkoutAssistant.Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            
            Startup.ConfigurationService(services: builder.Services, configuration: builder.Configuration);
            
            WebApplication app = builder.Build();
            
            Startup.Configuration(app: app);

            app.Run();
        }
    }
}