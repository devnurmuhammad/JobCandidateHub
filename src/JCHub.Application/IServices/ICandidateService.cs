using JCHub.Application.DTOs;

namespace JCHub.Application.IServices;

public interface ICandidateService
{
    public Task CreateOrUpdate(CandidateDto candidate);
    public CandidateDto? GetById(int id);
    public IEnumerable<CandidateDto> GetAll();
}