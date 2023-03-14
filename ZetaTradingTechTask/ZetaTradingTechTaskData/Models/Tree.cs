using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZetaTradingTechTaskData.Models;

[PrimaryKey(nameof(Id))]
public class Tree
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column(TypeName = "varchar(20)")]
    [Required]
    public string Name { get; set; } = null!;
}