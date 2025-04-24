using Core.Entities.Abstract;
using Newtonsoft.Json;

namespace Core.Entities.Concrete.Base;

public class BaseDto : IDto
{
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}