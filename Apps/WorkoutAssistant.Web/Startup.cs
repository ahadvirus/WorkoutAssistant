using System;
using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkoutAssistant.Web.Database.Connections;
using WorkoutAssistant.Web.Database.Contexts;
using WorkoutAssistant.Web.Infrastructures.Contracts;
using WorkoutAssistant.Web.Infrastructures.Database.Migrations;
using WorkoutAssistant.Web.Infrastructures.Extensions;

namespace WorkoutAssistant.Web;

public static class Startup
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    public static void ConfigurationService(IServiceCollection services, IConfiguration configuration)
    {
        //Add all sql connection to service

        services.AddTransient<MasterSqlConnection>(implementationFactory: _ =>
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
                        nameof(MasterSqlConnection)
                    }
                )
            ).Get<MasterSqlConnection>() ?? throw new Exception(message: "")
        );

        services.AddTransient<WorkoutSqlConnection>(implementationFactory: _ =>
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
                        nameof(WorkoutSqlConnection)
                    }
                )
            ).Get<WorkoutSqlConnection>() ?? throw new Exception(message: "")
        );

        //Implement Sql context to store all data to database
        services.AddDbContext<ApplicationContext>();

        //Add FluentMigration to accumulate all migration from application
        services.AddFluentMigratorCore()
            .ConfigureRunner(configure: builder => builder.AddSqlServer()
                .ScanIn(assemblies: new Assembly[] { typeof(Startup).Assembly }).For.All()
                .WithGlobalConnectionString(configureConnectionString: provider =>
                    provider.GetService<WorkoutSqlConnection>()!.ConnectionString)
            );

        //Add new logger fo FluentMigration for showing which migration is up
        services.AddLogging(configure: builder => builder.AddFluentMigratorConsole());
        
        //Add migrate service to run migration to database
        services.AddScoped<IMigratorService, MigrationService>();

        services.AddControllersWithViews();
    }

    /// <summary>
    /// Configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"><see cref="WebApplication"/></param>
    public static void Configuration(WebApplication app)
    {
        // Apply all migration to database
        app.UseMigrationUp();

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