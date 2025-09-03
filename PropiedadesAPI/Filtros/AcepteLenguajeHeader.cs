using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public sealed class AcepteLenguajeHeader : Attribute, IOperationFilter
{
    #region Metodos
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation ??= new OpenApiOperation();
        operation.Parameters ??= new List<OpenApiParameter>();

        var hasOnController = context.MethodInfo.DeclaringType?
            .GetCustomAttributes(inherit: true)
            .OfType<AcepteLenguajeHeader>()
            .Any() == true;

        var hasOnAction = context.MethodInfo
            .GetCustomAttributes(inherit: true)
            .OfType<AcepteLenguajeHeader>()
            .Any();

        if (!hasOnController && !hasOnAction)
            return;

        if (!operation.Parameters.Any(p =>
                p.In == ParameterLocation.Header &&
                p.Name.Equals("Token", StringComparison.OrdinalIgnoreCase)))
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "Token",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
        
    }
    #endregion
}
