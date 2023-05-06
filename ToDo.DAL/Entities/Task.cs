using System.ComponentModel.DataAnnotations;
using ToDo.DAL.Enum;

namespace ToDo.DAL.Entities;

[DisplayColumn("Tasks")]
public class TaskEntity
{
    [Key] 
    public int Id { get; set; }

    [Display(Name = "Task Name")]
    [Required]
    public string Name { get; set; }

    [Display(Name = "Task Priority")]
    [Required]
    public Priorety Priority { get; set; } = 0;
    
    [Display(Name = "Task Status")]
    [Required]
    public Status Status { get; set; }

    [Display(Name = "Action")] 
    [Required] 
    public string Action { get; set; }
    
    [Display(Name = "Expiration Date")]
    [Required]
    public DateTime? ExpirationDate { get; set; }
}