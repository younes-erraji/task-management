using TaskManagement.Data.VModels;
using TaskManagement.Data.Models;

namespace TaskManagement.Services.Contract
{
    public interface ITeachersRepository
    {
        List<Teacher> GetTeachers();
        Teacher CreateTeacher(TeacherVM teacher);
        int DeleteTeacher(Guid teacherId);
        Teacher EditTeacher(TeacherVM teacher, Guid teacherId);

        void CreateTeachers(List<Teacher> teachers);
        void DeleteTeachers();
    }
}
