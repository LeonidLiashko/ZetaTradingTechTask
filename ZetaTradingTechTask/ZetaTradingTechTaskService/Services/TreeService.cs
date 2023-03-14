using Microsoft.EntityFrameworkCore;
using ZetaTradingTechTaskData;
using ZetaTradingTechTaskData.Models;
using ZetaTradingTechTaskService.Interfaces;
using ZetaTradingTechTaskService.Models;

namespace ZetaTradingTechTaskService.Services;

public class TreeService : ITreeService
{
    public async Task<NodeModel> GetOrCreateTree(string treeName)
    {
        await using var context = new ZetaTradingTechTaskContext();
        var tree = await context.Trees.FirstOrDefaultAsync(x => x.Name == treeName);
        if (tree == null)
        {
            var newTree = new Tree
            {
                Name = treeName
            };
            context.Trees.Add(newTree);
            var firstNode = new Node
            {
                Tree = newTree,
                Name = treeName
            };
            context.Nodes.Add(firstNode);
            await context.SaveChangesAsync();
        }

        var allNodes = await context.Nodes.Where(x => x.Tree.Name == treeName).ToListAsync();
        var parent = new NodeModel(allNodes.First(x => x.ParentId == null));
        SetUpChildren(parent, allNodes);
        await context.SaveChangesAsync();
        return parent;
    }

    private static void SetUpChildren(NodeModel parent, IList<Node> allNodes)
    {
        parent.Children  = allNodes.Where(x => x.ParentId == parent.Id).Select(x => new NodeModel(x)).ToList();
        foreach (var child in parent.Children)
            SetUpChildren(child, allNodes);
    }
}