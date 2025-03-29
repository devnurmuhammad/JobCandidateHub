using JCHub.Application.DbInterfaces;
using JCHub.Application.IRepositories;
using JCHub.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace JCHub.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }

    public ICandidateRepository CandidateRepository => GetRepository<ICandidateRepository>();

    public TRepository GetRepository<TRepository>() where TRepository : notnull =>
        _serviceProvider.GetRequiredService<TRepository>();

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    public void Save() => _context.SaveChanges();
}