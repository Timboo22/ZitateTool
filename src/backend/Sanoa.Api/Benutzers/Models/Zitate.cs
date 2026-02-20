namespace SanoaAPI.Benutzers.Models;

public class Zitate
{ 
    public int Id { get; set; }

    public string ZitateName { get; set; }

    public int BenutzerId { get; set; }
    
    public Benutzer Benutzer { get; set; } = null!;
    
}