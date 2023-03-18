using Microsoft.EntityFrameworkCore;
using ZetaTradingTechTaskData;
using ZetaTradingTechTaskData.Models;
using ZetaTradingTechTaskService.Exceptions;
using ZetaTradingTechTaskService.Interfaces;
using ZetaTradingTechTaskService.Models;
using ZetaTradingTechTaskService.Parameters;

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

    public async Task<ExceptionModel> GetSingle(int id)
    {
        await using var context = new ZetaTradingTechTaskContext();
        var exceptionData = await context.Exceptions.FirstOrDefaultAsync(x => x.EventId == id);
        if (exceptionData == null)
            throw new SecureException($"No exception with Id: {id}");
        return new ExceptionModel(exceptionData);
    }

    public async Task<IEnumerable<ExceptionModelShort>> GetRange(int skip, int take, GetRangeParameters getRangeParameters)
    {
        await using var context = new ZetaTradingTechTaskContext();
        var exceptionsQuery = context.Exceptions.AsQueryable();
        if (getRangeParameters?.From != null)
            exceptionsQuery = exceptionsQuery.Where(x => x.CreatedAt >= getRangeParameters.From);
        if (getRangeParameters?.To != null)
            exceptionsQuery = exceptionsQuery.Where(x => x.CreatedAt <= getRangeParameters.To);
        if (getRangeParameters?.Search != null)
            exceptionsQuery = exceptionsQuery.Where(x => x.Message.Contains(getRangeParameters.Search));
        
        var exceptions = await exceptionsQuery.OrderBy(x => x.CreatedAt).Skip(skip).Take(take).ToListAsync();
        return exceptions.Select(x => new ExceptionModelShort(x)).ToList();
    }
}