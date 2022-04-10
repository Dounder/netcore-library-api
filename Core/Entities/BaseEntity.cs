namespace Core.Entities;

public class BaseEntity
{
    // Generate a guid instead of a sequential number
    public Guid Id { get; set; } 
    // Record creation date
    public DateTime CreatedAt { get; set; }
    // Record updated date
    public DateTime UpdatedAt { get; set; }
    // Record deactivation date
    public DateTime DisabledAt { get; set; }
    // Generate a guid instead of a sequential number
    public bool Enabled { get; set; }
}
