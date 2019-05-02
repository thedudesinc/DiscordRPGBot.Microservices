using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace DiscordRPGBot.BusinessLogic.Middleware
{
    public class SwaggerAddAPIKeyHeader : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                Name = "X-API-KEY",
                In = "header",
                Type = "string",
                Required = true
            });
        }
    }
}
