using Microsoft.EntityFrameworkCore;

using TaskManagement.Data.Models;
using TaskManagement.Data.VModels;
using TaskManagement.Services.Contract;

namespace TaskManagement.Services.EF.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskManagementDBContext _context;
        public TaskRepository(TaskManagementDBContext context)
        {
            _context = context;
        }

        public Data.Models.Task CreateTask(TaskVM taskVM)
        {
            Data.Models.Task task = new Data.Models.Task
            {
                Name = $"{taskVM.ActionType} des données de la table {taskVM.TableName}",
                ActionType = taskVM.ActionType,
                TableName = taskVM.TableName,
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task;
        }

        public TaskExecution ExecuteTask(Data.Models.Task task)
        {
            if (task is not null)
            {
                TaskExecution taskExecution = new TaskExecution
                {
                    TaskId = task.Id,
                    TaskStartDate = DateTime.Now,
                };

                _context.TasksExecution.Add(taskExecution);
                _context.SaveChanges();

                return taskExecution;
            }
            else
            {
                return null;
            }
        }

        public List<Data.Models.Task> GetTasks()
        {
            List<Data.Models.Task> tasks = _context.Tasks.ToList();

            return tasks;
        }

        public Data.Models.Task GetTask(Guid taskId)
        {
            var task = _context.Tasks.SingleOrDefault(t => t.Id == taskId);
            if (task is not null)
            {
                return task;
            }
            else
            {
                return null;
            }
        }

        public List<TaskExecution> GetTasksExecution()
        {
            List<TaskExecution> tasksExecution = _context.TasksExecution.Include(x => x.Task).ToList();

            return tasksExecution;
        }

        public TaskExecution CompleteTask(TaskExecution taskExecution)
        {
            taskExecution.TaskEndDate = DateTime.Now;
            _context.Update(taskExecution);

            return taskExecution;
        }
    }
}
