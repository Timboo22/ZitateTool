using SanoaAPI.Benutzers.Models;
using SanoaAPI.Benutzers.Services.Contracts;

namespace SanoaAPI.Extensions.BenutzerExtesions;

public static class EndpointsExtensions
{
    public static void BenutzerEndpoints(this WebApplication app)
    {
        app.MapGet("/holeExistierendenNutzer", (ContextDb db) => db.Benutzer.ToList());
        
        app.MapGet("/SuchBenutzer",
            (string suchbegriff, ContextDb db) => {
                return db.Benutzer.Where(s => s.Name.Contains(suchbegriff)).Take(3).ToList();
            }).DisableAntiforgery();
        
        app.MapPost("/LoescheBenutzer",
            (LoeschbarerNutzer benutzer, IBenutzerService benutzerService) => { benutzerService.BenutzerLoeschen(benutzer); });   
        
        
        app.MapPost("/benutzerHinzufuegen",
            (Benutzers.Models.Benutzer benutzer, IBenutzerService benutzerService) =>
            {
                benutzerService.BenutzerHinzufuegen(benutzer);
            });
    }
}