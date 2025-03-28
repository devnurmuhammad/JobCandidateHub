using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JCHub.Domain.Entities;

[Table("enum_state")]
public class State
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    [StringLength(30)]
    public required string Name { get; set; }
    
    public ICollection<Candidate>? Candidates { get; set; }
}