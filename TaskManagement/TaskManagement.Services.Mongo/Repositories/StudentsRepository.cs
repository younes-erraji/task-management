using MongoDB.Driver;

using TaskManagement.Services.Mongo.Settings;
using TaskManagement.Services.Contract;
using TaskManagement.Data.VModels;
using TaskManagement.Data.Models;

namespace TaskManagement.Services.Mongo.Repositories
{
  public class StudentsRepository : IStudentsRepository
  {
    private readonly IMongoCollection<Student> _students;

    public StudentsRepository(ITaskManagementDatabaseSettings settings, IMongoClient mongoClient)
    {
      IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
      _students = database.GetCollection<Student>("Students");
    }

    public Student CreateStudent(StudentVM studentVM)
    {
      Student student = new Student()
      {
        Name = studentVM.Name,
        BirthDate = studentVM.BirthDate,
        YearOfStudy = studentVM.YearOfStudy
      };

      _students.InsertOne(student);

      return student;
    }

    public void CreateStudents(List<Student> students)
    {
      _students.InsertMany(students);
    }

    public int DeleteStudent(Guid studentId)
    {
      Student student = _students.Find(s => s.Id == studentId).SingleOrDefault();
      if (student is not null)
      {
        _students.DeleteOne(s => s.Id == studentId);

        return 1;
      }
      else
      {
        return 0;
      }
    }

    public void DeleteStudents()
    {
      _students.DeleteMany(student => true);
    }

    public Student EditStudent(StudentVM studentVM, Guid studentId)
    {
      Student student = new Student()
      {
        Id = studentId,
        Name = studentVM.Name,
        BirthDate = studentVM.BirthDate,
        YearOfStudy = studentVM.YearOfStudy
      };

      _students.ReplaceOne(s => s.Id == studentId, student);

      return student;
    }
    public List<Student> GetStudents()
    {
      return _students.Find(student => true).ToList();
    }

    public Student GetStudent(Guid studentId)
    {
      return _students.Find(student => student.Id == studentId).SingleOrDefault();
    }
  }
}
