using SanoaAPI.Benutzers.Models;
using SanoaAPI.Benutzers.Services.Contracts;

public class BenutzerService : IBenutzerService
{
    private readonly IDataService _db;

    public BenutzerService(IDataService db)
    {
        _db = db;
    }

    public void BenutzerHinzufuegen(Benutzer benutzer)
    {
        try
        {
            if (string.IsNullOrEmpty(benutzer.Name))
                return;
            
            _db.Add(benutzer);
            _db.Save();
        }
        catch (Exception) { /* ignored */ }
    }

    public void BenutzerLoeschen(LoeschbarerNutzer benutzer)
    {
        try
        {
            if (benutzer.id == 0) return;
            
            var benutzerAusDb = _db.GetAll<Benutzer>()
                .FirstOrDefault(s => s.Id == benutzer.id);

            if (benutzerAusDb == null) return;

            _db.Remove(benutzerAusDb);
            _db.Save();
        }
        catch (Exception) { /* ignored */ }
    }
}