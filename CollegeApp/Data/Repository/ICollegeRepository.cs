using System.Linq.Expressions;

namespace CollegeApp.Data.Repository
{
    public interface ICollegeRepository<T>
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);

        //Task<T> GetByNameAsync(Expression<Func<T, bool>> filter);
        Task<T> CreateAsync(T DbRecord);

        Task<T> UpdateAsync(T DbRecord); 
        Task<bool> DeleteAsync(T DbRecord);
    }
}
