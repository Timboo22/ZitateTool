using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SanoaAPI;
using SanoaAPI.Benutzer.Avatar;
using SanoaAPI.Benutzer.Avatar.Services;
using SanoaAPI.Benutzer.Models;
using SanoaAPI.Benutzers.Services.Contracts;
using SanoaAPI.Extensions;
using SanoaAPI.Extensions.BenutzerExtesions;
using SanoaAPI.Extensions.ZitateExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBenutzerAvatarService, BenutzerAvatarService>();
builder.Services.AddScoped<IBenutzerService, BenutzerService>();
builder.Services.AddScoped<IDataService, DataService>();
builder.Services.Configure<AvatarOptions>(builder.Configuration);
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAngular", policy => {
        policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.MapOpenApi();
}

app.UseCors("AllowAngular");

app.UseStaticFiles(new StaticFileOptions {
    FileProvider = new PhysicalFileProvider(@"D:\AvatarSenoa"),
    RequestPath = "/avatars"
});

app.UseHttpsRedirection();

app.BenutzerEndpoints();
app.AvartarBenutzer();
app.ZitateEndpoints();

app.UseStaticFiles();

app.RunOutstandingMigrations();

app.Run();