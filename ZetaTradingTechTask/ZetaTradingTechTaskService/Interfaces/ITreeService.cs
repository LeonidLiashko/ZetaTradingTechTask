using ZetaTradingTechTaskData.Models;
using ZetaTradingTechTaskService.Models;

namespace ZetaTradingTechTaskService.Interfaces;

public interface ITreeService
{
    Task<NodeModel> GetOrCreateTree(string treeName);
}