using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.VModels
{
    public class TaskExecutionVM
    {
        [Required]
        public Guid TaskId { get; set; }
        [Required]
        public DateTime TaskStartDate { get; set; }
        public DateTime? TaskEndDate { get; set; }
    }
}
