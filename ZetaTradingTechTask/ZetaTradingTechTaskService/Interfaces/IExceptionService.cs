namespace ZetaTradingTechTaskService.Interfaces;

public interface IExceptionService
{
    Task<int> SaveException(DateTime createdAt, string parameters, string stackTrace, string message);
}