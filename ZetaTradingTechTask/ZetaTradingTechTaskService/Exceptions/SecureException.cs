namespace ZetaTradingTechTaskService.Exceptions;

public class SecureException : Exception
{
    public string Type { get; set; }
    
    public int Id { get; set; }

    public SecureException(string name, string type, int id) : base(name)
    {
        Type = type;
        Id = id;
    }
}