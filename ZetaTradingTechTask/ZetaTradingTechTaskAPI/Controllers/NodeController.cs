using Microsoft.AspNetCore.Mvc;
using ZetaTradingTechTaskService.Interfaces;
using ZetaTradingTechTaskService.Services;

namespace ZetaTradingTechTaskAPI.Controllers;

[ApiController]
[Route("api.user.tree")]
public class NodeController : Controller
{
    private readonly INodeService _nodeService;

    public NodeController(INodeService nodeService)
    {
        _nodeService = nodeService;
    }

    [HttpPost("create")]
    public async Task Create(string treeName, int parentNodeId, string nodeName)
    {
        await _nodeService.Create(treeName, parentNodeId, nodeName);
    }
    
    [HttpPost("delete")]
    public async Task Delete(string treeName, int nodeId)
    {
        await _nodeService.Delete(treeName, nodeId);
    }
    
    [HttpPost("rename")]
    public async Task Rename(string treeName, int nodeId, string newNodeName)
    {
        await _nodeService.Rename(treeName, nodeId, newNodeName);
    }
}