using Projekt_API__Avancerad.NET.Helper;

namespace Projekt_API__Avancerad.NET.Services.Interfaces
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        
    }
}
