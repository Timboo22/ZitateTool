using Microsoft.EntityFrameworkCore;
using SanoaAPI.Benutzers.Models;

namespace SanoaAPI;

public class ContextDb : DbContext
{
    public ContextDb(DbContextOptions<ContextDb> options):base(options) {}

    public DbSet<Benutzers.Models.Benutzer> Benutzer { get; set; }
    public DbSet<Zitate> Zitate { get; set; }
}