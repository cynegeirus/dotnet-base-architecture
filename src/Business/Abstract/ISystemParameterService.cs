using Core.Entities.Concrete.Management;
using Core.Utilities.Results;

namespace Business.Abstract;

public interface ISystemParameterService
{
    IDataResult<SystemParameter> Get(Guid id);
    IDataResult<List<SystemParameter>> GetList();
    IResult Add(SystemParameter requestDto);
    IResult Update(SystemParameter requestDto);
    IResult Delete(SystemParameter requestDto);
}