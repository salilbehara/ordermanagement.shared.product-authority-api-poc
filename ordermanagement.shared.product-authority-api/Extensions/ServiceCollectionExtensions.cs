using EBSCO.NetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ordermanagement.shared.product_authority_api.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ordermanagement.shared.product_authority_api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHealthCheckConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecksUI()
                .AddHealthChecks()
                .AddElasticsearch(configuration["HealthChecks:ErrorsLoggedOptions:Node"], "elasticSearch connection", HealthStatus.Unhealthy)
                .AddElasticsearchErrorsLoggedHealthCheck(o =>
                {
                    o.Node = configuration["HealthChecks:ErrorsLoggedOptions:Node"];
                    o.Index = configuration["HealthChecks:ErrorsLoggedOptions:Index"];
                }, "elasticsearch: errors logged", HealthStatus.Unhealthy);

            return services;
        }

        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    o.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath);
                o.OperationFilter<SwaggerDefaultValues>();
                o.ResolveConflictingActions(c => c.First());
                o.EnableAnnotations();
                o.SchemaFilter<SwaggerEnumNamesFilter>();
            });
            return services;
        }

        public static IServiceCollection AddWebApiVersioningConfiguration(this IServiceCollection services)
        {
            services.AddVersionedApiExplorer(o =>
            {
                o.GroupNameFormat = "'v'VVV";
                o.SubstituteApiVersionInUrl = true;
            });

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;

                // only increment webapi version when backwards compatibilty cannot be maintained
                #region Assign Controllers to API Version 1.0
                o.Conventions.Controller<ProductsController>().HasApiVersion(new ApiVersion(1, 0));
                #endregion
            });
            return services;
        }

        private static Info CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new Info()
            {
                Title = $"Product Authority API",
                Version = description.ApiVersion.ToString(),
                Description = "A microservice for the Product Authority.",
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }
    }
}
