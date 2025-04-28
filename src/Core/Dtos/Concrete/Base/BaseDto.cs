using Newtonsoft.Json;

namespace Core.Dtos.Concrete.Base;

public class BaseDto : IDto
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}