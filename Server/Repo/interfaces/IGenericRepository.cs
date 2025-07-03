namespace Server.Repo.interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public IQueryable<T> Query();
        public Task<T> GetByIdAsync(Guid id);
        public Task<int> AddAsync(T entity);
        public Task<int> UpdateAsync(T entity);
        public Task<int> DeleteAsync(Guid id);
    }
}
