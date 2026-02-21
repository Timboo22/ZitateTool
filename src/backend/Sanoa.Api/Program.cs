using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SanoaAPI;
using SanoaAPI.Benutzers.Models.Settings;
using SanoaAPI.Benutzers.Services;
using SanoaAPI.Benutzers.Services.Contracts;
using SanoaAPI.Extensions;
using SanoaAPI.Extensions.BenutzerExtesions;
using SanoaAPI.Extensions.LoginExtensions;
using SanoaAPI.Extensions.ZitateExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBenutzerService, BenutzerService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));


// builder.Services.AddScoped<IDataService, DataService>();
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

var tokenSettings = builder.Configuration.GetSection("TokenSettings").Get<TokenSettings>();

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
        ValidIssuer = tokenSettings?.ValidIssuer,
        ValidAudience = "token",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenSettings.IssuerSigningKey)),
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
    app.UseHttpsRedirection();
}


app.LoginEndpoint();
app.BenutzerEndpoints();
app.ZitateEndpoints();


app.UseDefaultFiles();
app.UseStaticFiles();
app.MapFallbackToFile("index.html");

app.RunOutstandingMigrations();

app.Run();