using NSubstitute;
using SanoaAPI.Benutzers.Models;
using SanoaAPI.Benutzers.Services.Contracts;

namespace SanoaAPI.Tests.BenutzerServiceTests;

public class BenutzerServiceTests
{
    
    
    [Fact]
    public void BenutzerHinzufuegen_ValidUser_AddsToDatabase()
    {
        var fakeService = Substitute.For<IBenutzerService>();

        var testUser = new Benutzers.Models.Benutzer { Id = 1, Name = "Timbo" };
        
        fakeService.BenutzerHinzufuegen(testUser);

        fakeService.Received(1).BenutzerHinzufuegen(testUser);
    }
    
    
    [Fact]
    public void BenutzerHinzufuegen_NameIstLeer_DarfNichtSpeichern()
    {
        var fakeService = Substitute.For<IBenutzerService>();

        var testUser = new Benutzers.Models.Benutzer { Id = 1, Name = "" };
        
        fakeService.BenutzerHinzufuegen(testUser);
        
        fakeService.DidNotReceive().BenutzerHinzufuegen(Arg.Any<Benutzers.Models.Benutzer>());
    }

    [Fact]
    public void BenutzerLoeschen_BenutzerWirdGeloescht()
    {
        var fakeService = Substitute.For<IBenutzerService>();

        var testUser = new LoeschbarerNutzer(500000);
        
        fakeService.BenutzerLoeschen(testUser);
        
        fakeService.Received(1).BenutzerLoeschen(testUser);
    } 
}