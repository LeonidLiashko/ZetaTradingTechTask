using ZetaTradingTechTaskData;
using ZetaTradingTechTaskData.Models;
using ZetaTradingTechTaskService.Interfaces;

namespace ZetaTradingTechTaskService.Services;

public class ExceptionService : IExceptionService
{
    public async Task<int> SaveException(DateTime createdAt, string parameters, string stackTrace, string message)
    {
        await using var context = new ZetaTradingTechTaskContext();
        var exception = new ExceptionData
        {
            CreatedAt = createdAt,
            Parameters = parameters[..Math.Min(200, parameters.Length)],
            StackTrace = stackTrace[..Math.Min(300, stackTrace.Length)],
            Message = message[..Math.Min(100, message.Length)]
        };
        context.Exceptions.Add(exception);
        await context.SaveChangesAsync();
        return exception.EventId;
    }
}