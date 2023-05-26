namespace Api.Controllers.AuthAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models;
using Api.Data;

public interface IUserService
{
    User Auth(string Email, string Password);

    User GetById(int id);    
    int? Register(User model);
}

public class UserService : IUserService
{

     private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public User Auth(string _Email, string _Password)
    {
        // var user = _users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);
        var user = _context.Users.SingleOrDefault(x => x.Email == _Email && x.Password == _Password);

        return user;
    }


    public User GetById(int id)
    {
        if (_context == null || _context.Users == null)
        {
            // Handle the case when the context or Users collection is null
            return null; // Return null or handle the error accordingly
        }
        
        return _context.Users.FirstOrDefault(x => x.UserId == id);
    }
    // helper methods


    public int? Register(User model)
    {
        try
        {
            Console.WriteLine("Register start" + model.ToString());
            // Add the user to the context and save changes
            _context.Users.Add(model);
            Console.WriteLine("Register start1 ProviderName="+_context.Database.ProviderName);


            _context.SaveChanges();

            // Return the UserID if the record is added successfully
            return model.UserId;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Register Exception " + ex.Message);

            throw new Exception("AttachUserToContext cannot be done." ,ex.InnerException );
        }
    }
}