using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DiscordRPGBot.BusinessLogic.Middleware
{
    public class APIKeyMessageHandlerMiddleware
    {
        private RequestDelegate _next;
        private readonly string API_KEY = Environment.GetEnvironmentVariable("API_KEY");

        public APIKeyMessageHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            bool validKey = false;

            var checkApiKeyExists = context.Request.Headers.ContainsKey("X-API-KEY");

            if(checkApiKeyExists)
            {
                if(context.Request.Headers["X-API-KEY"].Equals(API_KEY)) {
                    validKey = true;
                }
            }

            if(!validKey)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Invalid credentials!");
            }
            else
            {
                await _next.Invoke(context);
            }
        }
    }

    public static class MyHandlerExtensions
    {
        public static IApplicationBuilder UseAPIKeyMessageHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<APIKeyMessageHandlerMiddleware>();
        }
    }
}

