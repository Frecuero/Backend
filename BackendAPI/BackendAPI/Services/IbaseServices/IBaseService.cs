namespace BackendAPI.Services.IbaseServices
{
    public interface IBaseService<T>
    {
        Task<List<T>> GetList();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
    }
}
