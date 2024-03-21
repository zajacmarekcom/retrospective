namespace Retro.Common.Domain;

public interface IRepository<T>
{
    Task<T?> GetById(Guid id);
    void Add(T entity);
    void Update(T entity);
    Task Save();
}