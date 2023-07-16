using System;
using System.Reflection;
using FluentMigrator.Runner;
using Fluid.MvcViewEngine;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using WorkoutAssistant.Web.Areas.Auth.Controllers;
using WorkoutAssistant.Web.Database.Connections;
using WorkoutAssistant.Web.Database.Contexts;
using WorkoutAssistant.Web.Infrastructures.Contracts;
using WorkoutAssistant.Web.Infrastructures.Database.Migrations;
using WorkoutAssistant.Web.Infrastructures.Extensions;
using WorkoutAssistant.Web.Infrastructures.Localizer;
using WorkoutAssistant.Web.Infrastructures.Localizer.Models;

namespace WorkoutAssistant.Web;

public static class Startup
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/></param>
    /// <param name="configuration"><see cref="IConfiguration"/></param>
    /// <param name="environment"><see cref="IWebHostEnvironment"/></param>
    public static void ConfigurationService(IServiceCollection services, IConfiguration configuration,
        IWebHostEnvironment environment)
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

        //Implement Authentication to system
        services.AddAuthentication(defaultScheme: CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(configureOptions: options =>
            {
                options.LoginPath = string.Format(
                    format: "/{0}/{1}",
                    args: new object?[]
                    {
                        nameof(Models.Configurations.Names.Areas.Auth),
                        nameof(LoginController).RemoveController()
                    }
                );
                options.LogoutPath = string.Format(
                    format: "/{0}/{1}",
                    args: new object?[]
                    {
                        nameof(Models.Configurations.Names.Areas.Auth),
                        nameof(LogoutController).RemoveController()
                    }
                );
                options.AccessDeniedPath = string.Format(
                    format: "/{0}/{1}/{2}",
                    args: new object?[]
                    {
                        nameof(Models.Configurations.Names.Areas.Auth),
                        nameof(AccessController).RemoveController(),
                        nameof(AccessController.Denied)
                    }
                );
            });

        //Add Route configuration for asp.net system
        services.AddRouting(configureOptions: options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        });

        //Implement localization to services
        services.TryAdd(
            descriptor: new ServiceDescriptor(
                serviceType: typeof(IStringLocalizerFactory),
                implementationType: typeof(JsonLocalizerFactory),
                lifetime: ServiceLifetime.Singleton
            )
        );

        /*
        services.TryAdd(
            descriptor: new ServiceDescriptor(
                serviceType: typeof(IStringLocalizer),
                implementationType: typeof(JsonLocalizer),
                lifetime: ServiceLifetime.Singleton
            )
        );
        */

        // The address for searching localization json files
        services.AddSingleton<JsonLocalizationOption>(
            implementationInstance: new JsonLocalizationOption(
            
                path: System.IO.Path.Combine(paths: new string[]
                {
                    environment.WebRootPath,
                    nameof(LocalizationOptions.ResourcesPath)
                        .Replace(oldValue: nameof(System.IO.Path), newValue: string.Empty)
                })
            )
        );

        // Custom collection for localization service
        services.AddSingleton<ResourceCollection>();

        services.AddControllersWithViews()
            .AddFluid(setupAction: options =>
            {
                //options.Parser.
            })
            .AddViewLocalization(setupAction: options => { options.ResourcesPath = string.Empty; });
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