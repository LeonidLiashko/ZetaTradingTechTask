namespace ZetaTradingTechTaskService.Parameters;

public class GetRangeParameters
{
    public DateTime? From { get; set; } = null;

    public DateTime? To { get; set; } = null;

    public string? Search { get; set; } = null;
}