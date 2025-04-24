using Core.Entities.Concrete.Base;

namespace Entities.Dtos.Requests;

public class UserCreateRequestDto : BaseDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string? MailAddress { get; set; }
}