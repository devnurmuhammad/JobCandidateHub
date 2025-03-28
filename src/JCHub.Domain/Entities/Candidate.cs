using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JCHub.Domain.Entities;

[Table("doc_candidate")]
public class Candidate : BaseEntity
{
    [Column("first_name")]
    [StringLength(50)]
    public required string FirstName { get; set; } 
    
    [Column("last_name")]
    [StringLength(50)]
    public required string LastName { get; set; } 
    
    [Column("phone_number")]
    [StringLength(25)]
    public string? PhoneNumber { get; set; }
    
    [Column("email")]
    [StringLength(70)]
    public required string Email { get; set; } // Unique identifier
    
    [Column("preferred_call_time_from")]
    public TimeSpan? PreferredCallTimeFrom { get; set; }
    
    [Column("preferred_call_time_to")]
    public TimeSpan? PreferredCallTimeTo { get; set; }
    
    [Column("linkedin_profile")]
    [StringLength(100)]
    public string? LinkedInProfile { get; set; }
    
    [Column("github_profile")]
    [StringLength(100)]
    public string? GitHubProfile { get; set; }
    
    [Column("comment")]
    public required string Comment { get; set; }
    
    [ForeignKey("StateId")]
    public State State { get; set; }
}