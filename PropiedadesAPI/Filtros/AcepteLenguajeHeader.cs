using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PropiedadesAPI.Filtros
{
    /// <summary>
    /// Filtro de operación de Swagger que agrega automáticamente un parámetro de header "Token" a los endpoints
    /// </summary>
    /// <remarks>
    /// Este filtro personalizado se utiliza para:
    /// - Agregar un header opcional "Token" en la documentación de Swagger
    /// - Facilitar las pruebas de endpoints que requieren tokens adicionales
    /// - Proporcionar una interfaz consistente en Swagger UI
    /// 
    /// Se puede aplicar a nivel de controlador o método individual usando el atributo [AcepteLenguajeHeader]
    /// </remarks>
    /// <example>
    /// [AcepteLenguajeHeader]
    /// public class MiController : ControllerBase { ... }
    /// 
    /// [AcepteLenguajeHeader]
    /// public async Task&lt;ActionResult&gt; MiMetodo() { ... }
    /// </example>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AcepteLenguajeHeader : Attribute, IOperationFilter
    {
        #region Metodos
        /// <summary>
        /// Aplica el filtro a una operación de OpenAPI, agregando el parámetro de header "Token"
        /// </summary>
        /// <param name="operation">Operación de OpenAPI a modificar</param>
        /// <param name="context">Contexto que contiene información sobre el método y controlador</param>
        /// <remarks>
        /// El filtro verifica si el atributo está presente en el controlador o método,
        /// y solo agrega el parámetro si no existe ya uno con el mismo nombre.
        /// </remarks>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Validación de parámetros de entrada
            if (operation == null)
                return;

            // Inicializar lista de parámetros si no existe
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            // Verificar si el atributo está presente en el controlador
            var hasOnController = context.MethodInfo.DeclaringType?
                .GetCustomAttributes(typeof(AcepteLenguajeHeader), true)
                .Any() == true;

            // Verificar si el atributo está presente en el método
            var hasOnAction = context.MethodInfo
                .GetCustomAttributes(typeof(AcepteLenguajeHeader), true)
                .Any();

            // Solo aplicar el filtro si el atributo está presente
            if (!hasOnController && !hasOnAction)
                return;

            // Verificar que no exista ya un parámetro "Token" en los headers
            if (!operation.Parameters.Any(p =>
                    p.In == ParameterLocation.Header &&
                    p.Name.Equals("Token", StringComparison.OrdinalIgnoreCase)))
            {
                // Agregar el parámetro de header "Token"
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Token",
                    In = ParameterLocation.Header,
                    Required = false,
                    Description = "Token adicional para funcionalidades específicas",
                    Schema = new OpenApiSchema 
                    { 
                        Type = "string",
                        Example = new Microsoft.OpenApi.Any.OpenApiString("mi-token-personalizado")
                    }
                });
            }
        }
        #endregion
    }
}