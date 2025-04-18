using JCHub.Application.Constants;
using JCHub.Application.DbInterfaces;
using JCHub.Application.DTOs;
using JCHub.Application.IServices;
using JCHub.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace JCHub.Application.Implements.Services;

public class CandidateService : ICandidateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache?  _cache;

    public CandidateService(IUnitOfWork unitOfWork, IMemoryCache? cache = null)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task CreateOrUpdate(CandidateDto dto)
    {
        try
        {
            var existCandidate = _unitOfWork.CandidateRepository.GetAll()
                .FirstOrDefault(c => c.StateId == StateConst.Active && c.Email == dto.Email);
            if (existCandidate is null)
            {
                var candidate = new Candidate
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    LinkedInProfile = dto.LinkedInProfile,
                    GitHubProfile = dto.GitHubProfile,
                    StateId = StateConst.Active,
                    CreatedAt = DateTime.UtcNow.AddHours(5),
                    PreferredCallTimeFrom = dto.PreferredCallTimeFrom,
                    PreferredCallTimeTo = dto.PreferredCallTimeTo,
                    CreatedUserId = dto.CreatedUserId, 
                    Comment = dto.Comment,
                };

                await _unitOfWork.CandidateRepository.CreateAsync(candidate);
            }
            else
            {
                existCandidate.FirstName = dto.FirstName;
                existCandidate.LastName = dto.LastName;
                existCandidate.Email = dto.Email;
                existCandidate.PhoneNumber = dto.PhoneNumber;
                existCandidate.LinkedInProfile = dto.LinkedInProfile;
                existCandidate.GitHubProfile = dto.GitHubProfile;
                existCandidate.StateId = dto.StateId is 0 ? StateConst.Active : existCandidate.StateId;
                existCandidate.PreferredCallTimeFrom = dto.PreferredCallTimeFrom;
                existCandidate.PreferredCallTimeTo = dto.PreferredCallTimeTo;
                existCandidate.ModifiedAt = DateTime.UtcNow.AddHours(5);
                existCandidate.ModifiedUserId = 1;
                existCandidate.Comment = dto.Comment;
            }

            _unitOfWork.Save();
            
            // clear the cache, because the data was changed
            _cache.Remove("all_candidates");
        }
        catch (Exception ex)
        {
            throw new Exception("Error in CreateOrUpdate", ex);
        }
    }

    public CandidateDto? GetById(int id)
    {
        var result = new CandidateDto();

        var candidate = _unitOfWork.CandidateRepository.GetById(id);
        if (candidate is null)
            return null;

        result.Id = candidate.Id;
        result.FirstName = candidate.FirstName;
        result.LastName = candidate.LastName;
        result.Email = candidate.Email;
        result.PhoneNumber = candidate.PhoneNumber;
        result.LinkedInProfile = candidate.LinkedInProfile;
        result.GitHubProfile = candidate.GitHubProfile;
        result.StateId = candidate.StateId;
        result.CreatedAt = candidate.CreatedAt;
        result.PreferredCallTimeFrom = candidate.PreferredCallTimeFrom;
        result.PreferredCallTimeTo = candidate.PreferredCallTimeTo;
        result.CreatedUserId = candidate.CreatedUserId;
        result.Comment = candidate.Comment;

        return result;
    }

    public IEnumerable<CandidateDto> GetAll()
    {
        string cacheKey = "all_candidates";

        if (_cache.TryGetValue(cacheKey, out List<CandidateDto> cachedCandidates))
        {
            return cachedCandidates;
        }

        var result = new List<CandidateDto>();
        var candidates = _unitOfWork.CandidateRepository.GetAll();

        foreach (var candidate in candidates)
        {
            result.Add(new CandidateDto()
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                LinkedInProfile = candidate.LinkedInProfile,
                GitHubProfile = candidate.GitHubProfile,
                StateId = candidate.StateId,
                CreatedAt = candidate.CreatedAt,
                PreferredCallTimeFrom = candidate.PreferredCallTimeFrom,
                PreferredCallTimeTo = candidate.PreferredCallTimeTo,
                CreatedUserId = candidate.CreatedUserId,
                Comment = candidate.Comment,
            });
        }

        _cache.Set(cacheKey, result, TimeSpan.FromMinutes(5));

        return result;
    }
}