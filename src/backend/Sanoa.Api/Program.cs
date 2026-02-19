using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SanoaAPI;
using SanoaAPI.Benutzers.Services.Contracts;
using SanoaAPI.DTOs;
using SanoaAPI.Extensions;
using SanoaAPI.Extensions.BenutzerExtesions;
using SanoaAPI.Extensions.ZitateExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBenutzerService, BenutzerService>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "http://localhost:5202",
        ValidAudience = "token",
        IssuerSigningKey = new SymmetricSecurityKey("ein_sehr_geheimer_und_langer_schluessel_mit_mindestens_32_zeichen"u8.ToArray())
    };
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseCors("AllowAngular");
app.UseAuthentication();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(@"C:\Users\timlu\Documents\Sanoa.PictureSave"),
    RequestPath = "/avatars"
});

app.UseHttpsRedirection();

app.MapPost("auth/login", (loginParms loginParms, ContextDb dataService) =>
{
    var gefundenerNutzer = dataService.UserAuth.FirstOrDefault(s => s.username == loginParms.username);
    var passwort = builder.Configuration.GetSection("PasswordForValidUser").Value;

    if (gefundenerNutzer == null || loginParms.password != passwort)
        return Results.BadRequest("Falsche Anmedlung");
    
    var key = new SymmetricSecurityKey("ein_sehr_geheimer_und_langer_schluessel_mit_mindestens_32_zeichen"u8.ToArray());
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
    var token = new JwtSecurityToken(
        issuer: "http://localhost:5202",
        audience: "token",
        claims: new List<Claim>(),
        expires: DateTime.Now.AddMinutes(30),
        signingCredentials: creds);
    
    var tokenString =  new JwtSecurityTokenHandler().WriteToken(token);

    return Results.Ok(new {Token = tokenString});
});

app.BenutzerEndpoints();
app.ZitateEndpoints();

app.UseStaticFiles();

app.RunOutstandingMigrations();

app.Run();