using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SanoaAPI.Benutzers.Services.Contracts;

public interface IDataService
{
    IQueryable<T> GetAll<T>() where T : class;
    
    void Add<T>(T entity) where T : class;
    
    void Remove<T>(T entity) where T : class;
    
    void FirstOrDefault <T>(T entity) where T : class;
    
    void Save();
}