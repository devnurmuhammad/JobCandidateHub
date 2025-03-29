using JCHub.Application.IRepositories;
using JCHub.Domain.Entities;
using JCHub.Infrastructure.Data;

namespace JCHub.Infrastructure.Repositories;

public class CandidateRepository : BaseRepository<Candidate>, ICandidateRepository
{
    public CandidateRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}