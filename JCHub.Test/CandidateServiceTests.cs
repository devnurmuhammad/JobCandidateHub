using FluentAssertions;
using JCHub.Application.DTOs;
using JCHub.Application.Implements.Services;
using JCHub.Application.IRepositories;
using JCHub.Domain.Entities;
using Moq;
using JCHub.Application.DbInterfaces;

namespace JCHub.Test;

public class CandidateServiceTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
    private readonly CandidateService _candidateService;

    public CandidateServiceTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _candidateRepositoryMock = new Mock<ICandidateRepository>();

        _unitOfWorkMock.Setup(u => u.CandidateRepository).Returns(_candidateRepositoryMock.Object);

        _candidateService = new CandidateService(_unitOfWorkMock.Object);
    }

    /// <summary>
    /// add a new candidate
    /// </summary>
    [Fact]
    public async Task CreateOrUpdate_ShouldAddCandidate_WhenCandidateDoesNotExist()
    {
        var candidateDto = new CandidateDto
        {
            FirstName = "Nurmuhammad",
            LastName = "Davletov",
            Email = "nurmuhammad@example.com",
            LinkedInProfile = "https://linkedin.com/in/nurmuhammad",
            GitHubProfile = "https://github.com/nurmuhammad",
            PreferredCallTimeFrom = TimeSpan.FromHours(9),
            PreferredCallTimeTo = TimeSpan.FromHours(17),
            CreatedUserId = 1,
            Comment = "Potential candidate"
        };

        _candidateRepositoryMock
            .Setup(repo => repo.GetAll())
            .Returns(Enumerable.Empty<Candidate>().AsQueryable());

        await _candidateService.CreateOrUpdate(candidateDto);

        _candidateRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Candidate>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.Save(), Times.Once);
    }

    /// <summary>
    /// update exist candidate
    /// </summary>
    [Fact]
    public async Task CreateOrUpdate_ShouldUpdateCandidate_WhenCandidateExists()
    {
        var existingCandidate = new Candidate
        {
            FirstName = "Nurmuhammad",
            LastName = "Davletov",
            Email = "nurmuhammad@example.com",
            LinkedInProfile = "https://linkedin.com/in/nurmuhammad",
            GitHubProfile = "https://github.com/nurmuhammad",
            StateId = 1,
            PreferredCallTimeFrom = TimeSpan.FromHours(10),
            PreferredCallTimeTo = TimeSpan.FromHours(16),
            CreatedUserId = 1,
            Comment = "Existing candidate"
        };

        var candidateDto = new CandidateDto
        {
            FirstName = "Nurmuhammad",
            LastName = "Davletov",
            Email = "nurmuhammad@example.com",
            LinkedInProfile = "https://linkedin.com/in/nurmuhammad-updated",
            GitHubProfile = "https://github.com/nurmuhammad-updated",
            PreferredCallTimeFrom = TimeSpan.FromHours(9),
            PreferredCallTimeTo = TimeSpan.FromHours(17),
            CreatedUserId = 1,
            Comment = "Updated candidate"
        };

        _candidateRepositoryMock
            .Setup(repo => repo.GetAll())
            .Returns(new[] { existingCandidate }.AsQueryable());

        await _candidateService.CreateOrUpdate(candidateDto);

        _candidateRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Candidate>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.Save(), Times.Once);

        existingCandidate.LinkedInProfile.Should().Be("https://linkedin.com/in/nurmuhammad-updated");
        existingCandidate.GitHubProfile.Should().Be("https://github.com/nurmuhammad-updated");
        existingCandidate.PreferredCallTimeFrom.Should().Be(TimeSpan.FromHours(9));
        existingCandidate.PreferredCallTimeTo.Should().Be(TimeSpan.FromHours(17));
        existingCandidate.Comment.Should().Be("Updated candidate");
    }

    /// <summary>   
    /// exception handling test
    /// </summary>
    [Fact]
    public async Task CreateOrUpdate_ShouldThrowException_WhenUnitOfWorkFails()
    {
        var candidateDto = new CandidateDto
        {
            FirstName = "Test",
            LastName = "User",
            Email = "testuser@example.com",
            CreatedUserId = 1
        };

        _candidateRepositoryMock
            .Setup(repo => repo.GetAll())
            .Returns(Enumerable.Empty<Candidate>().AsQueryable());

        _unitOfWorkMock.Setup(u => u.Save()).Throws(new Exception("Database error"));

        Func<Task> action = async () => await _candidateService.CreateOrUpdate(candidateDto);

        await action.Should().ThrowAsync<Exception>().WithMessage("Database error");
    }
}