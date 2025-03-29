namespace JCHub.Application.DTOs;

public class CandidateDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; }
    public TimeSpan? PreferredCallTimeFrom  { get; set; }
    public TimeSpan? PreferredCallTimeTo { get; set; }
    public string? LinkedInProfile { get; set; }
    public string? GitHubProfile { get; set; }
    public string Comment { get; set; }
    public int StateId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CreatedUserId { get; set; }
}