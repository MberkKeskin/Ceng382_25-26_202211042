using System.ComponentModel.DataAnnotations;
namespace LabProject.Models
{
 public class Class
 {
 [Key]
 public int Id { get; set; }
 [Required]
 public string Name { get; set; } =string.Empty;
 [Required]
 public int StudentCount { get; set; }
 public string Description { get; set; }  =string.Empty;
 [Required]
 public bool IsActive { get; set; } = true;
 }
}