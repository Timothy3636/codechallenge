namespace Api.Controllers.AuthAPI;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models;
using Api.Data;

public interface IAdminService
{
    Admin Auth(string Email, string Password);

}

public class AdminService : IAdminService
{

     private readonly ApplicationDbContext _context;

    public AdminService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Admin Auth(string _Email, string _Password)
    {

        Console.WriteLine("Auth _Email="+_Email);

        // var user = _users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);
        var admin = _context.Admins.SingleOrDefault(x => x.Email == _Email && x.Password == _Password);

        return admin;
    }
}