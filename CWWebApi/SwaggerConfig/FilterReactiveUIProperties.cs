using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace CWWebApi.SwaggerConfig
{


    public class ApplyCustomSchemaFilters : ISchemaFilter
    {



        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {

            var excludeProperties = new[] { "thrownExceptions", "changed", "changing" };

            foreach (var prop in excludeProperties.Where(prop => schema.Properties.ContainsKey(prop)))
            {
                schema.Properties.Remove(prop);
            }
        }
    }
}



