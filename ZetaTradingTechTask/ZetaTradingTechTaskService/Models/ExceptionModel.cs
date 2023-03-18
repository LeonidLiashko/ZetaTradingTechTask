using ZetaTradingTechTaskData.Models;

namespace ZetaTradingTechTaskService.Models;

public class ExceptionModel
{
    public ExceptionModel(ExceptionData data)
    {
        EventId = data.EventId;
        CreatedAt = data.CreatedAt;
        Parameters = data.Parameters;
        StackTrace = data.StackTrace;
        Message = data.Message;
    }
    
    public int EventId { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public string Parameters { get; set; }
    
    public string StackTrace { get; set; }

    public string Message { get; set; }
}