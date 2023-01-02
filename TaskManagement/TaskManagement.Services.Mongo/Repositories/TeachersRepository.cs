using MongoDB.Driver;

using TaskManagement.Services.Mongo.Settings;
using TaskManagement.Services.Contract;
using TaskManagement.Data.VModels;
using TaskManagement.Data.Models;

namespace TaskManagement.Services.Mongo.Repositories
{
  public class TeachersRepository : ITeachersRepository
  {
    private readonly IMongoCollection<Teacher> _teachers;
    public TeachersRepository(ITaskManagementDatabaseSettings settings, IMongoClient mongoClient)
    {
      IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
      _teachers = database.GetCollection<Teacher>("Teachers");
    }

    public Teacher CreateTeacher(TeacherVM teacherVM)
    {
      Teacher teacher = new Teacher
      {
        Name = teacherVM.Name,
        BirthDate = teacherVM.BirthDate,
        MainSubjectTeaching = teacherVM.MainSubjectTeaching
      };

      _teachers.InsertOne(teacher);

      return teacher;
    }

    public void CreateTeachers(List<Teacher> teachers)
    {
      _teachers.InsertMany(teachers);
    }

    public int DeleteTeacher(Guid teacherId)
    {
      Teacher teacher = _teachers.Find(t => t.Id == teacherId).SingleOrDefault();
      if (teacher is not null)
      {
        _teachers.DeleteOne(t => t.Id == teacherId);

        return 1;
      }
      else
      {
        return 0;
      }
    }

    public void DeleteTeachers()
    {
      _teachers.DeleteMany(teacher => true);
    }

    public Teacher EditTeacher(TeacherVM teacherVM, Guid teacherId)
    {
      Teacher teacher = new Teacher
      {
        Id = teacherId,
        Name = teacherVM.Name,
        BirthDate = teacherVM.BirthDate,
        MainSubjectTeaching = teacherVM.MainSubjectTeaching
      };

      _teachers.ReplaceOne(s => s.Id == teacherId, teacher);

      return teacher;
    }
    public List<Teacher> GetTeachers()
    {
      return _teachers.Find(teacher => true).ToList();
    }

    public Teacher GetTeacher(Guid teacherId)
    {
      return _teachers.Find(teacher => teacher.Id == teacherId).SingleOrDefault();
    }
  }
}
