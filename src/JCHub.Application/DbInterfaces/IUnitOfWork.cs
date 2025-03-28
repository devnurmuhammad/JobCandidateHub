using JCHub.Application.IRepositories;

namespace JCHub.Application.DbInterfaces;

public interface IUnitOfWork
{
    ICandidateRepository  CandidateRepository { get; }
    
    TRepository GetRepository<TRepository>();
    Task<int> SaveAsync();
    void Save();
}