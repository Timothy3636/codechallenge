namespace Api.Controllers.AuthAPI;

using Api.Models;

public class RegisterRequest
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}


public class LoginRequest
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}
