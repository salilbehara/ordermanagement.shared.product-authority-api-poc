using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;

namespace ordermanagement.shared.product_authority_api.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationBuilderExtensions
    {
        private const string HEALTH_CHECKS_ROUTE = "/healthchecks";

        public static IApplicationBuilder UseHealthChecksMiddleware(this IApplicationBuilder app)
        {
            app.UseHealthChecks(HEALTH_CHECKS_ROUTE, new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status406NotAcceptable,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
            }).UseHealthChecksUI();

            return app;
        }

        public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger()
               .UseSwaggerUI(o =>
               {
                   foreach (var description in provider.ApiVersionDescriptions)
                   {
                       o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                   }
                   o.DisplayOperationId();
                   o.RoutePrefix = string.Empty;
               });

            return app;
        }
    }
}
