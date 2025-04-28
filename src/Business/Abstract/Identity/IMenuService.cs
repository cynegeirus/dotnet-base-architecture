using Core.Entities.Concrete.Identity;
using Core.Utilities.Results;

namespace Business.Abstract.Identity;

public interface IMenuService
{
    IDataResult<Menu> Get(Guid id);
    IDataResult<List<Menu>> GetList();
    IResult Add(Menu requestDto);
    IResult Update(Menu requestDto);
    IResult Delete(Menu requestDto);
}