using ZetaTradingTechTaskData.Models;

namespace ZetaTradingTechTaskService.Models;

public class NodeModel
{
    public NodeModel(Node node)
    {
        Id = node.Id;
        Name = node.Name;
    }

    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public IEnumerable<NodeModel> Children { get; set; }
}
