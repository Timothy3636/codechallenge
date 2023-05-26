
using Microsoft.AspNetCore.Mvc;
using Api.Models;

namespace Api.Controllers.AuthAPI
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        
        private readonly IJwtService _jwtService;

        public AdminController(IAdminService adminService, IJwtService jwtService)
        {
            _adminService = adminService;
            _jwtService = jwtService;
        }

        [HttpPost("adminAuth")]
        public IActionResult Authenticate([FromBody] LoginRequest request)
        {
            Console.WriteLine("Authenticate token="+request.Email);

            var response = _adminService.Auth(request.Email, request.Password);

            if (response == null)
                return BadRequest(new { message = "Invalid username or password" });

            // Generate JWT token
            var token = _jwtService.GenerateToken(response);

            Console.WriteLine("AttachUserToContext token="+token);

            // Call the AttachUserToContext method from the caller class (MyController)
            _jwtService.AttachUserToContext(HttpContext, token);


            // Include the token in the response body
            return Ok(new { access_token = token });
        }

        [HttpGet("adminAuth")]
        public IActionResult GetUserFromToken()
        {

            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            Console.WriteLine("GetUserFromToken token="+token);

            // Generate JWT token
            Admin userFromToken = (Admin) _jwtService.DecodeToken(token);

            // Include the token in the response body
            return Ok( userFromToken );
        }


    }

    
}
