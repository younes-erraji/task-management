using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.Models
{
    public class Teacher
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(50)]
        public string MainSubjectTeaching { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
