using Microsoft.EntityFrameworkCore;
using SanoaAPI.Benutzers.Models;
using SanoaAPI.Benutzers.Services.Contracts;

namespace SanoaAPI;

public class ContextDb : DbContext
{
    public ContextDb(DbContextOptions<ContextDb> options):base(options) {}

    public DbSet<Benutzers.Models.Benutzer> Benutzer { get; set; }
    public DbSet<Zitate> Zitate { get; set; }
}