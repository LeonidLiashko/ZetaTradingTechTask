using Microsoft.AspNetCore.Mvc;
using ZetaTradingTechTaskService.Interfaces;
using ZetaTradingTechTaskService.Models;

namespace ZetaTradingTechTaskAPI.Controllers;

[ApiController]
[Route("api.user.tree")]
public class TreeController : Controller
{
    private readonly ITreeService _treeService;

    public TreeController(ITreeService treeService)
    {
        _treeService = treeService;
    }
    
    [HttpPost("get")]
    public async Task<NodeModel> Get(string treeName)
    {
        return await _treeService.GetOrCreateTree(treeName);
    }
}