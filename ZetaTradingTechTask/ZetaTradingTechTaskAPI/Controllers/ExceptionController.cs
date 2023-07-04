using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ZetaTradingTechTaskService.Interfaces;
using ZetaTradingTechTaskService.Models;
using ZetaTradingTechTaskService.Parameters;

namespace ZetaTradingTechTaskAPI.Controllers;

[ApiController]
[Route("api.user.journal")]
public class ExceptionController : Controller
{
    private readonly IExceptionService _exceptionService;

    public ExceptionController(IExceptionService exceptionService)
    {
        _exceptionService = exceptionService;
    }

    [HttpPost("getSingle")]
    public async Task<ExceptionModel> GetSingle(int id)
    {
        return await _exceptionService.GetSingle(id);
    }
    
    [HttpPost("getRange")]
    public async Task<IEnumerable<ExceptionModelShort>> GetRange(
        int skip, int take,[FromBody] GetRangeParameters parameters)
    {
        return await _exceptionService.GetRange(skip, take, parameters);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("error")]
    public async Task<ExceptionModel> HandleError()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (exception == null)
            throw new Exception("Can't find exception");
        var parameters = await GetParameters(HttpContext);
        var data = $"{{\"message\": \"{exception.Message}\"}}";
        return await _exceptionService.SaveException(DateTime.UtcNow, parameters, exception.StackTrace ?? "", data);
    }

    private static async Task<string> GetParameters(HttpContext context)
    {
        var queryParameters = string.Join(
            ',', context.Request.Query.Select(x => $"{x.Key}: {x.Value}"));
        string bodyStr;
        using (var reader
               = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
        {
            bodyStr = await reader.ReadToEndAsync();
        }

        //context.HttpContext.Request.Body.Position = 0;
        var parameters = $"{queryParameters}; {bodyStr}";
        return parameters;
    }
}