using Core.Dtos.Concrete;

namespace Entities.Dtos.Requests;

public class UserUpdateRequestDto : BaseDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? MailAddress { get; set; }
}