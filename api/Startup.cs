using Microsoft.Extensions.DependencyInjection;
using Api.Controllers.ContractAPI;
using Api.Controllers.AuthAPI;
using Api.Controllers.Common;
using Api.Middleware;
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Server.Kestrel.Core;


var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("Configurations/appsettings.json");
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
string connectionString = configuration.GetConnectionString("DefaultConnection");

// var builder = CreateHostBuilder(args).Build().Run();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IContractService, ContractService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString ));

builder.Services.AddLogging(loggingBuilder =>
    {
        loggingBuilder.AddConfiguration(configuration.GetSection("Logging")); // Read logging configuration from appsettings.json    
        loggingBuilder.AddConsole(); // Configure console logging
        loggingBuilder.AddDebug(); // Configure debug output logging
        loggingBuilder.AddSerilog(Log.Logger); // Add Serilog as the logging provider
    });


builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactCorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:3000" , "https://localhost:3001" ) // Replace with your React application's domain
                .AllowAnyHeader()
                .AllowAnyMethod();
    });
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ListenAnyIP(7135, listenOptions =>
    {
        listenOptions.UseHttps(new HttpsConnectionAdapterOptions
        {
            ServerCertificate = new X509Certificate2(
                "Configurations/certificate.pfx", "12345678")
        });
    });
});


var app = builder.Build();

app.UseCors("ReactCorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();

// app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<ErrorHandlingMiddleware>();
// app.UseMiddleware<JwtMiddleware>();

app.Run();

