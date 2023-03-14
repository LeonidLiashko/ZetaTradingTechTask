using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZetaTradingTechTaskData.Models;

[Index(nameof(TreeId))]
[PrimaryKey(nameof(Id))]
public class Node
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int? ParentId { get; set; }
    
    public Node Parent { get; set; } = null!;
    
    [Required]
    public int TreeId { get; set; }
    
    public Tree Tree { get; set; } = null!;

    [Column(TypeName = "varchar(20)")]
    [Required]
    public string Name { get; set; } = null!;
}