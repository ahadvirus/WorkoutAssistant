using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkoutAssistant.Web.Database.Connections;
using WorkoutAssistant.Web.Database.Contexts;

namespace WorkoutAssistant.Web;

public static class Startup
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigurationService(IServiceCollection services, IConfiguration configuration)
    {
        //Add all sql connection to service
        services.AddTransient<GymSqlConnection>(implementationFactory: _ =>
            configuration.GetSection(
                key: string.Format(
                    format: "{0}:{1}",
                    args: new object?[]
                    {
                        string.Format(
                            format: "{0}{1}",
                            args: new object?[]
                            {
                                nameof(Database),
                                nameof(Database.Connections)
                            }
                        ),
                        nameof(GymSqlConnection)
                    }
                )
            ).Get<GymSqlConnection>() ?? throw new Exception(message: "")
        );

        //Implement Sql context to store all data to database
        services.AddDbContext<ApplicationContext>();

        services.AddControllersWithViews();
    }

    /// <summary>
    /// Configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"></param>
    public static void Configuration(WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
}