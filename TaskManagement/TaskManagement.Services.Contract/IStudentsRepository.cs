using TaskManagement.Data.VModels;
using TaskManagement.Data.Models;

namespace TaskManagement.Services.Contract
{
    public interface IStudentsRepository
    {
        List<Student> GetStudents();
        Student CreateStudent(StudentVM student);
        int DeleteStudent(Guid studentId);
        Student EditStudent(StudentVM student, Guid studentId);

        void CreateStudents(List<Student> students);
        void DeleteStudents();
    }
}
