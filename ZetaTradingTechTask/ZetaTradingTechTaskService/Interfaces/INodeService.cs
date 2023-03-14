namespace ZetaTradingTechTaskService.Interfaces;

public interface INodeService
{
    Task Create(string treeName, int parentNodeId, string nodeName);

    Task Delete(string treeName, int nodeId);

    Task Rename(string treeName, int nodeId, string newNodeName);
}