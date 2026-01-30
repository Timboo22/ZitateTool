using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using SanoaAPI;
using SanoaAPI.Benutzer.Avatar;
using SanoaAPI.Benutzer.Avatar.Services;
using SanoaAPI.Benutzer.Models;
using SanoaAPI.Benutzers.Models;
using SanoaAPI.Benutzers.Services;
using SanoaAPI.Benutzers.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IBenutzerAvatarService, BenutzerAvatarService>();
builder.Services.AddScoped<IBenutzerService, BenutzerService>();
builder.Services.Configure<AvatarOptions>(builder.Configuration);
builder.Services.AddDbContext<ContextDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

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

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(@"D:\AvatarSenoa"),
    RequestPath = "/avatars"
});

app.UseHttpsRedirection();

app.MapPost("/benutzerHinzufuegen",
    (Benutzer benutzer, IBenutzerService benutzerService) => { benutzerService.BenutzerHinzufuegen(benutzer); });

app.MapPost("/avatarBildUpload",
    ([FromForm] IFormFile file, IBenutzerAvatarService benutzerAvatarService) =>
    {
        benutzerAvatarService.SpeicherBildImOrdner(file);
    }).DisableAntiforgery();

app.MapPost("/LoescheBenutzer",
    (LoeschbarerNutzer benutzer, IBenutzerService benutzerService) => { benutzerService.BenutzerLoeschen(benutzer); });

app.MapPost("/erstelleZitat", async (Zitate zitat, ContextDb db) => 
    { 
        db.Zitate.Add(zitat); 
        await db.SaveChangesAsync(); 
    
        return Results.Created($"/zitat/{zitat.Id}", zitat); 
    })
    .DisableAntiforgery();

app.MapGet("/holeExistierendenNutzer", (ContextDb db) => db.Benutzer.ToList());

app.MapGet("/SuchBenutzer",
    (string suchbegriff, ContextDb db) =>
    {
        return db.Benutzer.Where(s => s.Name.Contains(suchbegriff)).Take(3).ToList();
    }).DisableAntiforgery();


app.MapGet("/holeZitate",(ContextDb db) => db.Zitate.ToList());

app.UseStaticFiles();

app.Run();