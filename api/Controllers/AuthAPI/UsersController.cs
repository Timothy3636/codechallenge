
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers.AuthAPI
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        private readonly IJwtService _jwtService;

        private readonly ILogger<UserController> _logger;


        public UserController(IUserService userService, 
        IJwtService jwtService , ILogger<UserController>  logger)
        {
            _userService = userService;
            _jwtService = jwtService;            
            _logger = logger;
        }

        [HttpPost("auth")]
        public IActionResult Authenticate(LoginRequest model)
        {
            var response = _userService.Auth(model.Email, model.Password);

            if (response == null)
                return BadRequest(new { message = "Invalid username or password" });

            // Generate JWT token
            var token = _jwtService.GenerateToken(response);

            // Call the AttachUserToContext method from the caller class (MyController)
            _jwtService.AttachUserToContext(HttpContext, token);


            Console.WriteLine("XXX AttachUserToContext token="+token);

            // Include the token in the response body
            return Ok(new { access_token = token });
 
        }

        [HttpGet("auth")]
        public IActionResult GetUserFromToken()
        {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            Console.WriteLine("GetUserFromToken token="+token);

            // Generate JWT token
            User userFromToken = (User) _jwtService.DecodeToken(token);

            // Include the token in the response body
            return Ok( userFromToken );
        }



        [HttpGet("healthcheck")]
        public IActionResult healthcheck()
        {

            // Include the token in the response body
            return Ok(new { message = "Health Check start" });
        }        
        
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest model)
        {
            Console.WriteLine("register enter in console");

            _logger.LogInformation("register enter" );
            

            try
            {
                // Perform registration logic using the UserService
                // You can access the necessary methods from the IUserService interface
                var newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    IsActive = true,
                    RegistrationDate = DateTime.Now
                };

                // Call the appropriate method from the UserService
                var registeredUserId = _userService.Register(newUser);

                if (registeredUserId != null)
                {

                    Console.WriteLine("register registeredUserId=" + registeredUserId);

                    // Registration successful, return the registered user ID
                    return Ok(new { UserId = registeredUserId });
                }
                else
                {
                    // Registration failed
                    return BadRequest(new { message = "Registration failed" });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during user registration");
                return StatusCode(500, new { message = "Internal server error" });
            }             
        }
    }
}
