namespace BookStoreAPI.Data.Repository.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T entity);
        Task UpdateAsync();
        Task DeleteAsync(T entity);
        Task<T> GetByIdAsync(Guid id);
        IQueryable<T> GetAll();
    }
}
