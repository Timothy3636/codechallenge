namespace Api.Middleware;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Api.Controllers.AuthAPI;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next  )
    {
        _next = next;
        // _appSettings = appSettings.Value;
    }

    public async Task Invoke(HttpContext context, IJwtService jwtService)
    {

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        Console.WriteLine("JwtMiddleware Invoke token=" + token);


            if (token != null)
            {
                jwtService.AttachUserToContext(context, token);
            }
            else
            {
                if  ( !IsExcludedApiCall(context)) 
                {
                        
                    Console.WriteLine(" Return Unauthorized response" + token);
                    // Return Unauthorized response if the token is missing
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    return;                        
                }
            }               


 

        await _next(context);
    }
    // Helper method to check if the API call should be excluded from authentication
    private bool IsExcludedApiCall(HttpContext context)
    {

        Console.WriteLine(" IsExcludedApiCall start");
        // Add conditions to exclude specific API calls from authentication
        var excludedPaths = new[] { "/api/user/register", 
        "/api/user/auth" ,
        "/api/admin/auth" 
        };
        var requestPath = context.Request.Path.Value;

        Console.WriteLine(" IsExcludedApiCall requestPath="+requestPath);

        return excludedPaths.Any(path => 
        requestPath.StartsWith(path, StringComparison.OrdinalIgnoreCase));
    }
}