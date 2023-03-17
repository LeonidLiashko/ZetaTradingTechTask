using Microsoft.EntityFrameworkCore;
using ZetaTradingTechTaskData;
using ZetaTradingTechTaskData.Models;
using ZetaTradingTechTaskService.Exceptions;
using ZetaTradingTechTaskService.Interfaces;

namespace ZetaTradingTechTaskService.Services;

public class NodeService : INodeService
{
    public async Task Create(string treeName, int parentNodeId, string nodeName)
    {
        await using var context = new ZetaTradingTechTaskContext();
        
        var tree = await CheckGetTree(treeName, context);

        var parent = await context.Nodes.FirstOrDefaultAsync(x => x.Id == parentNodeId);
        if (parent == null)
            throw new SecureException($"Parent node must exists. No node with id {parentNodeId}");
        
        await SiblingNameCheck(parentNodeId, nodeName, context);

        var newNode = new Node
        {
            Parent = parent,
            Name = nodeName,
            Tree = tree
        };
        context.Nodes.Add(newNode);
        await context.SaveChangesAsync();
    }

    public async Task Delete(string treeName, int nodeId)
    {
        await using var context = new ZetaTradingTechTaskContext();

        //check tree exist
        await CheckGetTree(treeName, context);
        
        var children = context.Nodes.Where(x => x.ParentId == nodeId);
        if (children.Any())
            throw new SecureException($"You have to delete all children nodes first. Parent node Id: {nodeId}");
        
        var node = await CheckGetNode(nodeId, context);

        context.Nodes.Remove(node);
        await context.SaveChangesAsync();
    }

    public async Task Rename(string treeName, int nodeId, string newNodeName)
    {
        await using var context = new ZetaTradingTechTaskContext();
        
        //check tree exist
        await CheckGetTree(treeName, context);

        var node = await CheckGetNode(nodeId, context);

        await SiblingNameCheck(node.ParentId, newNodeName, context);

        node.Name = newNodeName;
        await context.SaveChangesAsync();
    }

    private static async Task<Node> CheckGetNode(int nodeId, ZetaTradingTechTaskContext context)
    {
        var node = await context.Nodes.FirstOrDefaultAsync(x => x.Id == nodeId);
        if (node == null)
            throw new SecureException($"Node does not exist. Node Id: {nodeId}");
        return node;
    }

    private static async Task<Tree> CheckGetTree(string treeName, ZetaTradingTechTaskContext context)
    {
        var tree = await context.Trees.FirstOrDefaultAsync(x => x.Name == treeName);
        if (tree == null)
            throw new SecureException($"No tree with name: {treeName}");
        return tree;
    }

    private static async Task SiblingNameCheck(int? parentNodeId, string nodeName, ZetaTradingTechTaskContext context)
    {
        var siblings = await context.Nodes.Where(x => x.ParentId == parentNodeId && x.Name == nodeName).ToListAsync();
        if (siblings.Any())
            throw new SecureException($"A new node name must be unique across all siblings. Node name: {nodeName}");
    }
}