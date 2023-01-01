using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.Models
{
    public class Task
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? Name { get; set; }
        [Required]
        public string TableName { get; set; }
        [Required]
        public string ActionType { get; set; }
    }
}
