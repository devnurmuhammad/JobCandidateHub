namespace JCHub.Application.IRepositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    public Task CreateAsync(TEntity entity);
    public void Update(TEntity entity);
    public TEntity? GetById(int id);
    public IQueryable<TEntity> GetAll();
    public void Delete(int id);
    Task SaveChangesAsync();
}