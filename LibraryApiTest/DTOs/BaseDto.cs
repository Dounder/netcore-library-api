namespace Api.DTOs;

public class BaseDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow.Date;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow.Date;
    public DateTime DisabledAt { get; set; } = DateTime.UtcNow.Date;
    public bool Enabled { get; set; } = true;
}
