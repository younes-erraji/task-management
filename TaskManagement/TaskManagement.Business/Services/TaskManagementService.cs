using TaskManagement.Data.Models;
using TaskManagement.Services.Contract;

namespace TaskManagement.Business.Services
{
  public class TaskManagementService : ITaskManagementService
  {
    private readonly ITaskRepository _tasksRepository;
    private readonly IStudentsRepository _studentsRepository;
    private readonly ITeachersRepository _teachersRepository;
    public TaskManagementService(ITaskRepository tasksRepository, IStudentsRepository studentsRepository, ITeachersRepository teachersRepository)
    {
      _tasksRepository = tasksRepository;
      _studentsRepository = studentsRepository;
      _teachersRepository = teachersRepository;
    }

    public TaskExecution DeleteAllDataTask(Data.Models.Task task)
    {
      TaskExecution taskExecution = _tasksRepository.ExecuteTask(task);

      if (task.TableName.Equals("students", StringComparison.OrdinalIgnoreCase))
      {
        _studentsRepository.DeleteStudents();
      }
      else
      {
        _teachersRepository.DeleteTeachers();
      }

      // _tasksRepository.CompleteTask(taskExecution);

      return taskExecution;
    }

    public TaskExecution AddRandomDataTask(Data.Models.Task task)
    {
      TaskExecution taskExecution = _tasksRepository.ExecuteTask(task);

      if (task.TableName.Equals("students", StringComparison.OrdinalIgnoreCase))
      {
        List<Student> students = new List<Student>();

        for (int i = 0; i < 500; i++)
        {
          Student student = new Student()
          {
            Name = Faker.Name.FullName(),
            BirthDate = DateTime.Now,
            YearOfStudy = Faker.RandomNumber.Next(1, 2023),
            CreationDate = DateTime.Now,
          };

          students.Add(student);
        }

        _studentsRepository.CreateStudents(students);
      }
      else if (task.TableName.Equals("teachers", StringComparison.OrdinalIgnoreCase))
      {
        List<Teacher> teachers = new List<Teacher>();

        for (int i = 0; i < 500; i++)
        {
          Teacher teacher = new Teacher()
          {
            Name = Faker.Name.FullName(),
            BirthDate = DateTime.Now,
            MainSubjectTeaching = Faker.Company.Name(),
            CreationDate = DateTime.Now,
          };

          teachers.Add(teacher);
        }

        _teachersRepository.CreateTeachers(teachers);
      }

      // _tasksRepository.CompleteTask(taskExecution);

      return taskExecution;
    }
  }
}
