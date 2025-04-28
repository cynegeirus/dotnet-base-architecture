using Core.Entities.Abstract;
using Newtonsoft.Json;

namespace Core.Dtos.Concrete;

public class BaseDto : IDto
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}