namespace Api.DTOs;

internal class AuthorDto : BaseDto
{
    public string FirstName { get; set; } = "unknown";
    public string LastName { get; set; } = "unknown";
}
