using TaskManagement.Services.Contract;
using TaskManagement.Data.VModels;
using TaskManagement.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Services.EF.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private readonly TaskManagementDBContext _context;
        public StudentsRepository(TaskManagementDBContext context)
        {
            _context = context;
        }
        public Student CreateStudent(StudentVM studentVM)
        {
            Student student = new Student
            {
                Name = studentVM.Name,
                BirthDate = studentVM.BirthDate,
                YearOfStudy = studentVM.YearOfStudy
            };

            _context.Students.Add(student);
            _context.SaveChanges();

            return student;
        }
        public int DeleteStudent(Guid studentId)
        {
            Student student = _context.Students.SingleOrDefault(s => s.Id == studentId);

            if (student is not null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();

                return 1;
            }
            else
            {
                return 0;
            }
        }
        public Student EditStudent(StudentVM studentVM, Guid studentId)
        {
            Student student = _context.Students.SingleOrDefault(st => st.Id == studentId);

            if (student is not null)
            {
                student.Name = studentVM.Name;
                student.BirthDate = studentVM.BirthDate;
                student.YearOfStudy = studentVM.YearOfStudy;

                _context.Update(student);
                _context.SaveChanges();

                return student;
            }
            else
            {
                return null;
            }
        }
        public List<Student> GetStudents()
        {
            List<Student> students = _context.Students.ToList();
            return students;
        }

        public void CreateStudents(List<Student> students)
        {
            _context.Students.AddRange(students);
            _context.SaveChanges();
        }

        public void DeleteStudents()
        {
            // Delete All Students
            _context.Database.ExecuteSqlRaw($"TRUNCATE TABLE Students");
        }
    }
}
