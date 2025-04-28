using Core.Dtos.Concrete.Base;

namespace Entities.Dtos.Requests;

public class RegisterRequestDto : BaseDto
{
    public required string? FirstName { get; set; }
    public required string? LastName { get; set; }
    public required string? MailAddress { get; set; }
    public required string? Username { get; set; }
    public required string? Password { get; set; }
}