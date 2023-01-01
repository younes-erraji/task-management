using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.VModels
{
    public class TaskVM
    {
        [Required]
        public string TableName { get; set; }
        [Required]
        public string ActionType { get; set; }
    }
}
