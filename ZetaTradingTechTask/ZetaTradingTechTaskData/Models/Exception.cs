using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZetaTradingTechTaskData.Models;


[PrimaryKey(nameof(EventId))]
public class ServerException
{
    public  int EventId { get; set; }
    
    public long DateTime { get; set; }
    
    [Column(TypeName = "varchar(100)")]
    [Required]
    public string Parameters { get; set; } = null!;
    
    [Column(TypeName = "varchar(300)")]
    [Required]
    public string StackTrace { get; set; } = null!;
}