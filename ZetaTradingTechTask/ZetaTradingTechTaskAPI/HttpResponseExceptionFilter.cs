using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ZetaTradingTechTaskService.Exceptions;
using ZetaTradingTechTaskService.Interfaces;

namespace ZetaTradingTechTaskAPI;

public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    private readonly IExceptionService _exceptionService;

    public HttpResponseExceptionFilter(IExceptionService exceptionService)
    {
        _exceptionService = exceptionService;
    }

    public int Order => int.MaxValue - 10;

    public void OnActionExecuting(ActionExecutingContext context) { }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null) 
            return;
        
        var isSecure = context.Exception is SecureException;
        var type = isSecure ? "Secure" : "Exception";
        var parameters = GetParametrs(context);
        var data = $"{{\"message\": \"{context.Exception.Message}\"}}";
        var id = _exceptionService.SaveException(DateTime.UtcNow, parameters, context.Exception.StackTrace ?? "", data).Result;
        context.Result = new ObjectResult($"\"type\": \"{type}\", \"id\": \"{id}\", \"data\": {data}")
        {
            StatusCode = 500
        };

        context.ExceptionHandled = true;
    }

    private static string GetParametrs(ActionContext context)
    {
        var queryParameters = string.Join(
            ',', context.HttpContext.Request.Query.Select(x => $"{x.Key}: {x.Value}"));
        string bodyStr;
        using (var reader
               = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8, true, 1024, true))
        {
            bodyStr = reader.ReadToEnd();
        }

        //context.HttpContext.Request.Body.Position = 0;
        var parameters = $"{queryParameters}; {bodyStr}";
        return parameters;
    }
}