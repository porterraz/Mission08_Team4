using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission08_Team4.Models
{
    public class TaskItem
    {
        [Key]
        [Required]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Task name is required")]
        public string TaskName { get; set; }

        public string? DueDate { get; set; }

        [Required(ErrorMessage = "Quadrant is required")]
        [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4")]
        public int Quadrant { get; set; }

        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public bool Completed { get; set; }
    }
}