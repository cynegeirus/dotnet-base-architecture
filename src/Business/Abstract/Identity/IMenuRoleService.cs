using Core.Entities.Concrete.Identity;
using Core.Utilities.Results;

namespace Business.Abstract.Identity;

public interface IMenuRoleService
{
    IDataResult<MenuRole> Get(Guid id);
    IDataResult<List<MenuRole>> GetList();
    IResult Add(MenuRole requestDto);
    IResult Update(MenuRole requestDto);
    IResult Delete(MenuRole requestDto);
}