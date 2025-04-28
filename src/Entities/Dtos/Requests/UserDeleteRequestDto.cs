using Core.Dtos.Concrete;

namespace Entities.Dtos.Requests;

public class UserDeleteRequestDto : BaseDto
{
    public Guid Id { get; set; }
}