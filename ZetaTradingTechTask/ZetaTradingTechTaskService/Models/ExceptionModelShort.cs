using ZetaTradingTechTaskData.Models;

namespace ZetaTradingTechTaskService.Models;

public class ExceptionModelShort
{
    public ExceptionModelShort(ExceptionData data)
    {
        EventId = data.EventId;
        CreatedAt = data.CreatedAt;
    }
    
    public int EventId { get; set; }
    
    public DateTime CreatedAt { get; set; }
}