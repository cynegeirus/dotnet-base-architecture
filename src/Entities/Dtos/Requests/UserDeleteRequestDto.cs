using Core.Dtos.Concrete.Base;

namespace Entities.Dtos.Requests;

public class UserDeleteRequestDto : BaseDto
{
    public Guid Id { get; set; }
}