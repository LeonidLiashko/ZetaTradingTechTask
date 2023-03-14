using Microsoft.AspNetCore.Mvc;
using ZetaTradingTechTaskService.Services;

namespace ZetaTradingTechTaskAPI.Controllers;

[ApiController]
[Route("api.user.tree")]
public class NodeController : Controller
{
    private readonly NodeService _nodeService;

    public NodeController(NodeService nodeService)
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