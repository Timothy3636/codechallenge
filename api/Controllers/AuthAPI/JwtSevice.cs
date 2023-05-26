
namespace Api.Controllers.AuthAPI;

using Microsoft.Extensions.Options;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Api.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Api.Data;

public interface IJwtService
{
    string GenerateToken(UserBase user);

    // void jwtService.attachUserToContext(context, token);
    public void AttachUserToContext(HttpContext context, string token);

    public UserBase DecodeToken(string token);
}


public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<JwtService> _logger;
    private readonly IUserService _userService;

    public JwtService(IConfiguration configuration, 
        IUserService userService,
        ILogger<JwtService> logger)
    {
        _configuration = configuration;
        _logger = logger;        
        _userService = userService;
    }

    public string GenerateToken(UserBase input_user)
    {
        User user = (User) input_user;

        // Console.WriteLine("GenerateToken start");
        var secretKey = _configuration["AppSettings:Secret"];
        // Console.WriteLine("GenerateToken start secretKey="+secretKey);
        double ttlDay  = 0;
        try
        {
            string ttlDayValue = _configuration.GetSection("AppSettings:TTL_DAY").Value;
            ttlDay = Convert.ToDouble(ttlDayValue);
            // Use the ttlDay variable for further processing
            Console.WriteLine("GenerateToken start ttlDayValue="+ttlDayValue);
        }
        catch (Exception ex)
        {
             Console.WriteLine("An error occurred while converting the TTL_DAY value: " + ex.Message);
            throw new Exception("GenerateToken cannot be done." ,ex.InnerException );
        }

        // Console.WriteLine("GenerateToken start token=");

        // Generate JWT token using the secret key        
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("IsActive", user.IsActive.ToString()),
                new Claim("RegistrationDate", user.RegistrationDate.ToString()),
                // Add more claims as needed
            }),
            Expires = DateTime.UtcNow.AddDays(ttlDay),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        // Console.WriteLine("GenerateToken start token="+token);
        string tokenValue = tokenHandler.WriteToken(token);
        Console.WriteLine("GenerateToken start tokenValue="+tokenValue);

        return tokenValue;
    }

    public UserBase DecodeToken(string token)
    {

        if (string.IsNullOrEmpty(token))
        {
            throw new Exception("Invalid token");
        }

        Console.WriteLine("DecodeToken start");
        Console.WriteLine("DecodeToken start token="+token);
        var secretKey = _configuration["AppSettings:Secret"];
        Console.WriteLine("DecodeToken start secretKey="+secretKey);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

        try
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            var userBaseId = int.Parse(claimsPrincipal.FindFirst("UserId")?.Value);
            var email = claimsPrincipal.FindFirst("Email")?.Value;
            var firstName = claimsPrincipal.FindFirst("FirstName")?.Value;
            var lastName = claimsPrincipal.FindFirst("LastName")?.Value;
            var isActive = bool.Parse(claimsPrincipal.FindFirst("IsActive")?.Value);
            

            var user = new User
            {
                UserId = userBaseId,
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                IsActive = isActive,
            };

            return user;
        }
        catch (Exception ex)
        {
            // Handle the exception, such as logging or throwing a custom exception
            Console.WriteLine("An error occurred while decoding the token: " + ex.Message);
            // return null;
            throw new Exception("DecodeToken cannot be done." ,ex.InnerException );

        }
    }


    public void AttachUserToContext(HttpContext context, string token)
    {
        Console.WriteLine("AttachUserToContext start="+token);

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(
                _configuration.GetSection("AppSettings:Secret").Value
                );
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "UserId").Value);

            // // attach user to context on successful jwt validation
            context.Items["User"] = _userService.GetById(userId);
        }
        catch (Exception ex)
        {
            throw new Exception("AttachUserToContext cannot be done." ,ex.InnerException );
        }
    }    
}
