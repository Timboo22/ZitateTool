using SanoaAPI.Benutzers.Models;

namespace SanoaAPI.Benutzers.Services.Contracts;

public interface IBenutzerService
{
    public void BenutzerHinzufuegen(Benutzer benutzer);

    public void BenutzerLoeschen(LoeschbarerNutzer benutzer);
}