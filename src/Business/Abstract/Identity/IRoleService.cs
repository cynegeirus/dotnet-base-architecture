using Core.Entities.Concrete.Identity;
using Core.Utilities.Results;

namespace Business.Abstract.Identity;

public interface IRoleService
{
    IDataResult<Role> Get(Guid id);
    IDataResult<List<Role>> GetList();
    IResult Add(Role requestDto);
    IResult Update(Role requestDto);
    IResult Delete(Role requestDto);
}