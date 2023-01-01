using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Data.VModels
{
    public class TeacherVM
    {
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required, StringLength(50)]
        public string MainSubjectTeaching { get; set; }
    }
}
