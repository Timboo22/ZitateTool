namespace SanoaAPI.Benutzers.Services.Contracts;

public class DataService : IDataService
{
    private readonly ContextDb _context;

    public DataService(ContextDb context)
    {
        _context = context;
    }
    
    public void Add<T>(T entity) where T : class => _context.Add(entity);
    
    public IQueryable<T> GetAll<T>() where T : class => _context.Set<T>();
    
    public void Remove<T>(T entity) where T : class => _context.Remove(entity);
    
    public void Save() => _context.SaveChanges();
}