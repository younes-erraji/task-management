using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagement.Data.Models
{
    public class TaskExecution
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Guid TaskId { get; set; }
        [Required]
        public DateTime TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }

        [ForeignKey(nameof(TaskId))]
        public virtual Task Task { get; set; }
    }
}
