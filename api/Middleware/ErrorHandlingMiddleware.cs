using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics;

using System;
using System.Threading.Tasks;

namespace Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Call the next middleware in the pipeline
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                // Log the exception or perform any other error handling logic
                // var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                // if (exceptionHandlerFeature != null)
                // {
                //     var logger = context.RequestServices.GetRequiredService<ILogger<Startup>>();
                //     logger.LogError(exceptionHandlerFeature.Error, "An error occurred during the request");
                // }                
                
                // // Set the response status code to indicate the error
                // context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                
                // // Set the response content type
                // context.Response.ContentType = "text/plain";
                
                // // Write the error message to the response body
                // await context.Response.WriteAsync("An error occurred. Please try again later.");
            }
        }
    }
}
