using System;
using System.IO;
using System.Linq;
using System.Net;
using FluentMigrator.Runner;
using Fluid.MvcViewEngine;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
using WorkoutAssistant.Web.Infrastructures.Translators.Globalization;
using WorkoutAssistant.Web.Infrastructures.Translators.Localizations;
using WorkoutAssistant.Web.Infrastructures.Translators.Localizations.Models;
using WorkoutAssistant.Web.Infrastructures.Web.Routes;
using WorkoutAssistant.Web.Infrastructures.Web.Routes.Conventions;
using WorkoutAssistant.Web.Models.Configurations;

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
                .ScanIn(assemblies: new[] { typeof(Startup).Assembly }).For.All()
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
                options.LoginPath = nameof(Named.Routes.Auth.Login).GetRoutePathByName();
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

        //Grab all route from assembly
        services.AddSingleton(
            implementationInstance: RouteCollection.GetInstance(typeof(Startup).Assembly)
            );

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
        services.AddSingleton(
            implementationInstance: new JsonLocalizationOption(
                path: Path.Combine(paths: new[]
                {
                    environment.WebRootPath,
                    nameof(LocalizationOptions.ResourcesPath)
                        .Replace(oldValue: nameof(Path), newValue: string.Empty),
                    nameof(Infrastructures.Translators.Localizations)
                })
            )
        );

        // Custom collection for localization service
        services.AddSingleton<ResourceCollection>();
        
        // The address for searching localization json files
        services.AddSingleton(
            implementationInstance: new LanguageOption(
                address: Path.Combine(paths: new[]
                {
                    environment.WebRootPath,
                    nameof(LocalizationOptions.ResourcesPath)
                        .Replace(oldValue: nameof(Path), newValue: string.Empty),
                    nameof(Infrastructures.Translators.Globalization)
                })
            )
        );
        
        services.AddSingleton<Infrastructures.Translators.Globalization.LanguageCollection>();

        services.AddControllersWithViews(configure: options =>
                options.Conventions.Insert(index: 0, item: new ReplaceNameAndTemplateToPathConvention()))
            .AddFluid()
            .AddViewLocalization(setupAction: options => { options.ResourcesPath = string.Empty; });
    }

    /// <summary>
    /// Configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"><see cref="WebApplication"/></param>
    public static void Configuration(WebApplication app)
    {
        // Apply all migration to database
        //app.UseMigrationUp();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.Use(middleware: async (context, next) =>
        {
            await next(context: context);

            context.Response.StatusCode = (int)HttpStatusCode.NotFound;

            
            await context.Response.WriteAsync(text: string.Format(
                    format:
                    "<!DOCTYPE html><html lang=\"en\"><head><meta charset=\"UTF-8\"><meta name=\"viewport\" content=\"width=device-width,initial-scale=1\"><title>{0}</title></head><body><h1>{1}</h1></body></html>",
                    args: new object?[]
                    {
                        nameof(HttpStatusCode.NotFound), 
                        context.GetFullRoutePath()
                    }
                )
            );
        });

        using (IServiceScope scope = app.Services.CreateScope())
        {
            Infrastructures.Translators.Globalization.LanguageCollection? collection = scope.ServiceProvider
                .GetService<Infrastructures.Translators.Globalization.LanguageCollection>();
            if (collection != null && collection.Any())
            {
                app.UseRequestLocalization(optionsAction: options =>
                {
                    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(collection.Select(selector: language => language.Code).First());
                    options.SupportedCultures = collection.Select(selector: language => language.CultureInfo).ToArray();
                    options.SupportedUICultures = collection.Select(selector: language => language.CultureInfo).ToArray();
                });
            }
        }
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        /*
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        */

        //app.map
    }
}