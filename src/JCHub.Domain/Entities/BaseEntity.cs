using System.ComponentModel.DataAnnotations.Schema;

namespace JCHub.Domain.Entities;

public class BaseEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("state_id")]
    public int StateId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("created_user_id")]
    public int CreatedUserId { get; set; }

    [Column("modified_at")]
    public DateTime? ModifiedAt { get; set; }

    [Column("modified_user_id")]
    public int? ModifiedUserId { get; set; }
}