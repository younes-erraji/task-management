using TaskManagement.Services.Contract;
using TaskManagement.Data.VModels;
using TaskManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Services.EF.Repositories
{
  public class TeachersRepository : ITeachersRepository
  {
    private readonly TaskManagementDBContext _context;
    public TeachersRepository(TaskManagementDBContext context)
    {
      _context = context;
    }

    public Teacher CreateTeacher(TeacherVM teacherVM)
    {
      Teacher teacher = new Teacher
      {
        Name = teacherVM.Name,
        BirthDate = teacherVM.BirthDate,
        MainSubjectTeaching = teacherVM.MainSubjectTeaching
      };

      _context.Teachers.Add(teacher);
      _context.SaveChanges();

      return teacher;
    }
    public int DeleteTeacher(Guid teacherId)
    {
      Teacher teacher = _context.Teachers.SingleOrDefault(t => t.Id == teacherId);

      if (teacher is not null)
      {
        _context.Teachers.Remove(teacher);
        _context.SaveChanges();

        return 1;
      }
      else
      {
        return 0;
      }
    }
    public Teacher EditTeacher(TeacherVM teacherVM, Guid teacherId)
    {
      Teacher teacher = _context.Teachers.SingleOrDefault(t => t.Id == teacherId);

      if (teacher is not null)
      {
        teacher.Name = teacherVM.Name;
        teacher.BirthDate = teacherVM.BirthDate;
        teacher.MainSubjectTeaching = teacherVM.MainSubjectTeaching;

        _context.Update(teacher);
        _context.SaveChanges();

        return teacher;
      }
      else
      {
        return null;
      }
    }
    public List<Teacher> GetTeachers()
    {
      List<Teacher> teachers = _context.Teachers.ToList();
      return teachers;
    }

    public Teacher GetTeacher(Guid teacherId)
    {
      Teacher teacher = _context.Teachers.SingleOrDefault(s => s.Id == teacherId);
      return teacher;
    }

    public void CreateTeachers(List<Teacher> teachers)
    {
      _context.Teachers.AddRange(teachers);
      _context.SaveChanges();
    }

    public void DeleteTeachers()
    {
      // Delete All Teachers
      _context.Database.ExecuteSqlRaw($"TRUNCATE TABLE Teachers");
    }
  }
}
