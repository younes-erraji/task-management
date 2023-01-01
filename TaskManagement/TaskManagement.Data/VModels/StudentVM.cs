using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.VModels
{
    public class StudentVM
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required, Range(0, 2024)]
        public int YearOfStudy { get; set; }
    }
}
