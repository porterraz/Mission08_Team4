using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission08_Team4.Models
{
    // Data model for the Eisenhower Matrix task management system
    public class TaskItem
    {
        [Key]
        [Required]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Task name is required")]
        public string TaskName { get; set; } = string.Empty;

        // Optional - user can leave blank
        public string? DueDate { get; set; }

        // Quadrant values: 1=Urgent/Important, 2=Not Urgent/Important, 3=Urgent/Not Important, 4=Neither
        [Required(ErrorMessage = "Quadrant is required")]
        [Range(1, 4, ErrorMessage = "Quadrant must be between 1 and 4")]
        public int Quadrant { get; set; }

        // Link to Category table
        [ForeignKey("Category")]
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        public bool Completed { get; set; }
    }
}
