using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class RemoveSchemasDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {

        var schemasToRemove = new List<string> { "UserLoginRequest", "UserUpdateRequest" };
        foreach (var schema in schemasToRemove)
        {
            swaggerDoc.Components.Schemas.Remove(schema);
        }
    }
}

