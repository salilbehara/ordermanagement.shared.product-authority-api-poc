using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ordermanagement.shared.product_authority_api
{
    [ExcludeFromCodeCoverage]
    public class SwaggerEnumNamesFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies a schema filter which exports enumeration names, instead of only the values, via the x-enumNames extension. This can
        /// be read by NSwag client utilities.
        /// </summary>
        /// <param name="schema">The schema to apply the filter to.</param>
        /// <param name="context">The current schema filter context.</param>
        public void Apply(Schema schema, SchemaFilterContext context)
        {
            var typeInfo = context.SystemType.GetTypeInfo();

            if (typeInfo.IsEnum)
            {
                var names = Enum.GetNames(context.SystemType);

                schema.Extensions.Add("x-enumNames", names);
            }
        }
    }
}
