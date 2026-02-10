using System;
using SanoaAPI.Benutzers.Models;
using SanoaAPI.Benutzers.Services.Contracts;

namespace SanoaAPI.Benutzers.Services;
public class BenutzerService : IBenutzerService
{
    private readonly ContextDb _db;

    public BenutzerService(ContextDb db)
    {
        _db = db;
    }

    public void BenutzerHinzufuegen(Models.Benutzer benutzer)
    {
        try
        {
            _db.Add(benutzer);
            _db.SaveChanges();
        }
        catch (Exception ex)
        {
            // ignored
        }
    }

    public void BenutzerLoeschen(LoeschbarerNutzer benutzer)
    {
        try
        {
            if (benutzer.id != 0) return;
    
            var benutzerAusDb = _db.Benutzer.FirstOrDefault(s  => benutzer.id == s.Id);
    
            if (benutzerAusDb == null) return;
    
            _db.Benutzer.Remove(benutzerAusDb);
            _db.SaveChanges();
        }
        catch (Exception e)
        {
            // ignored
        }
    }
}