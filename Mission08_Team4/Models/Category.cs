using System.ComponentModel.DataAnnotations;

namespace Mission08_Team4.Models
{
    // Categories: Home, Work, School, Church (predefined set for this app)
    public class Category
    {
        [Key]
        [Required]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
