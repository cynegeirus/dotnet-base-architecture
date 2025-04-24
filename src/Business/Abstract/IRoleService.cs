using Core.Entities.Concrete.Management;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface IRoleService
{
    IDataResult<Role> Get(Guid id);
    IDataResult<List<Role>> GetList();
    IResult Add(Role requestDto);
    IResult Update(Role requestDto);
    IResult Delete(Role requestDto);
}