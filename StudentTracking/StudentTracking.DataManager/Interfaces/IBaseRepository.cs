namespace StudentTracking.DataManager.Interfaces;

public interface IBaseRepository<T> where T : class
{
    public Task CreateAsync(T item);
    public Task<T> UpdateAsync(T item);
    public Task DeleteAsync(T item);
    public Task<T> GetByIdAsync(Guid id);
    public Task<IEnumerable<T>> GetAllAsync();
}