using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CWWebApi.SwaggerConfig
{
    public class RemoveSchemasFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Components.Schemas.Remove("IReactiveObjectIReactivePropertyChangedEventArgsIObservable");

            swaggerDoc.Components.Schemas.Remove("ExceptionIObservable");

        }
    }

}



