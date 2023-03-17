using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace ZetaTradingTechTaskData.Models;


[PrimaryKey(nameof(EventId))]
public class ExceptionData
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EventId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    [Column(TypeName = "varchar(200)")]
    [Required]
    public string Parameters { get; set; } = null!;
    
    [Column(TypeName = "varchar(300)")]
    [Required]
    public string StackTrace { get; set; } = null!;

    [Column(TypeName = "varchar(100)")]
    [Required]
    public string Message { get; set; }
}