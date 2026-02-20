using System.Linq.Expressions;

namespace SanoaAPI.Benutzers.Services.Contracts;

public interface IDataService
{
    IQueryable<T> GetAll<T>() where T : class;
    
    void Add<T>(T entity) where T : class;
    
    void Remove<T>(T entity) where T : class;

    T? FirstOrDefault<T>(Expression<Func<T, bool>> predicate) where T : class;
    
    void Save();
}