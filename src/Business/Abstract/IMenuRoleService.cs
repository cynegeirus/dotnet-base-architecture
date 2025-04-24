using Core.Entities.Concrete.Management;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface IMenuRoleService
{
    IDataResult<MenuRole> Get(Guid id);
    IDataResult<List<MenuRole>> GetList();
    IResult Add(MenuRole requestDto);
    IResult Update(MenuRole requestDto);
    IResult Delete(MenuRole requestDto);
}