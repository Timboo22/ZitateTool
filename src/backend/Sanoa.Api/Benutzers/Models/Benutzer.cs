using System.ComponentModel.DataAnnotations.Schema;

namespace SanoaAPI.Benutzers.Models;

public class Benutzer
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int Lehrjahr { get; set; }

    public string LieblingsZitat { get; set; }

    public ICollection<Zitate> Zitate { get; set; } = new List<Zitate>();
    
    [NotMapped]
    
    public int AusbildungsEnde { get; set; }

}