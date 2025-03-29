using JCHub.Application.IRepositories;
using JCHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace JCHub.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;
    
    public BaseRepository(AppDbContext appDbContext)
    {
        _dbContext = appDbContext;
        _dbSet = appDbContext.Set<TEntity>();
    }

    public async Task CreateAsync(TEntity entity) => await _dbSet.AddAsync(entity);

    public TEntity? GetById(int id) => _dbSet.Find(id);

    public IQueryable<TEntity> GetAll() => _dbSet;

    public void Update(TEntity entity) => _dbSet.Update(entity);

    public async Task SaveChangesAsync() => await _dbContext.SaveChangesAsync();

    public void Delete(int id) => _dbContext.Remove(id);
}