using NSubstitute;
using SanoaAPI.Benutzers.Models;
using SanoaAPI.Benutzers.Services;
using SanoaAPI.Benutzers.Services.Contracts;

namespace Sanoa.Tests;

public class BenutzerServiceTests
{
    private readonly IDataService _fakeDb;
    private readonly BenutzerService _fakedService;

    public BenutzerServiceTests()
    {
        _fakeDb = Substitute.For<IDataService>();
        _fakedService = new BenutzerService(_fakeDb);
    }

    [Fact]
    public void BenutzerLoeschen_UserExists_CallsRemoveAndSave()
    {
        var dbBenutzer = new Benutzer { Id = 1, Name = "Timbo" };
        var loeschNutzer = new LoeschbarerNutzer(1);
        var benutzerListe = new List<Benutzer> { dbBenutzer }.AsQueryable();
        _fakeDb.GetAll<Benutzer>().Returns(benutzerListe);
        _fakedService.BenutzerLoeschen(loeschNutzer);
        _fakeDb.Received(1).Remove(dbBenutzer);
        _fakeDb.Received(1).Save();
    }

    [Fact]
    public void BenutzerHinzufuegen_Hinzufuegen_Ohne_Namen()
    {
        var neuerUser = new Benutzer { Id = 1 };

        _fakedService.BenutzerHinzufuegen(neuerUser);
        _fakeDb.DidNotReceive().Add(neuerUser);
        _fakeDb.DidNotReceive().Save();
    }

    [Fact]
    public void BenutzerHinzufuegen_ValidUser_CallsAddAndSave()
    {
        var fakeDb = Substitute.For<IDataService>();
        var sut = new BenutzerService(fakeDb);
        var neuerUser = new Benutzer { Id = 1, Name = "Timbo" };

        sut.BenutzerHinzufuegen(neuerUser);
        fakeDb.Received(1).Add(neuerUser);
        fakeDb.Received(1).Save();
    }
}