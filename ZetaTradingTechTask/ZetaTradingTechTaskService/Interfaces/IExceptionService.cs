using ZetaTradingTechTaskService.Models;
using ZetaTradingTechTaskService.Parameters;

namespace ZetaTradingTechTaskService.Interfaces;

public interface IExceptionService
{
    Task<ExceptionModel> SaveException(DateTime createdAt, string parameters, string stackTrace, string message);
    
    Task<ExceptionModel> GetSingle(int id);
    Task<IEnumerable<ExceptionModelShort>> GetRange(int skip, int take, GetRangeParameters getRangeParameters);
}