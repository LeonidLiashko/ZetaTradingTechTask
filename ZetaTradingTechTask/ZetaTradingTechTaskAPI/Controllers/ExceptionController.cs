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
}