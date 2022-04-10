using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class BaseEntity
{
    // Generate a guid instead of a sequential number
    public Guid Id { get; set; } 
    // Record creation date
    [Column(TypeName = "date")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.Date;
    // Record updated date
    [Column(TypeName = "date")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.Date;
    // Record deactivation date
    [Column(TypeName = "date")]
    public DateTime DisabledAt { get; set; } = DateTime.UtcNow.Date;
    // Generate a guid instead of a sequential number
    public bool Enabled { get; set; } = true;
}
