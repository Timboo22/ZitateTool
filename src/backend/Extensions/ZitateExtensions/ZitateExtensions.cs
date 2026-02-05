using SanoaAPI.Benutzers.Models;

namespace SanoaAPI.Extensions.ZitateExtensions;

public static class ZitateExtensions
{
    public static void ZitateEndpoints(this WebApplication app)
    {
        app.MapPost("/erstelleZitat", async (Zitate postZitat, ContextDb db) =>
            {
                Zitate zitat = new Zitate()
                {
                    ZitateName =  postZitat.ZitateName,
                    BenutzerId = postZitat.BenutzerId,
                };          
                
                db.Zitate.Add(zitat);
                await db.SaveChangesAsync();

                return Results.Created($"/zitat/{zitat.Id}", zitat);
            })
            .DisableAntiforgery();
        
        app.MapGet("/holeZitate", (ContextDb db) => db.Zitate.ToList());
    }
}