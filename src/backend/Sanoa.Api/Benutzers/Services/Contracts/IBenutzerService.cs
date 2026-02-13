using SanoaAPI.Benutzers.Models;

namespace SanoaAPI.Benutzers.Services.Contracts;

public interface IBenutzerService
{
    public void BenutzerHinzufuegen(Benutzers.Models.Benutzer benutzer);

    public void BenutzerLoeschen(LoeschbarerNutzer benutzer);
}