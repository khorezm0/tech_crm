namespace DAL.Contracts
{
    public interface IRepository<T>
    {
        public Task<T> CreateAsync(T obj);
        public Task DeleteAsync(T obj);

        public Task<T> UpdateAsync(T obj);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task<T> GetByIdAsync(string id);
    }
}
