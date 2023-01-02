using MongoDB.Driver;

using TaskManagement.Data.Models;
using TaskManagement.Data.VModels;
using TaskManagement.Services.Contract;
using TaskManagement.Services.Mongo.Settings;

namespace TaskManagement.Services.Mongo.Repositories
{
    public class TasksRepository : ITaskRepository
    {
        private readonly IMongoCollection<Data.Models.Task> _tasks;
        private readonly IMongoCollection<TaskExecution> _tasksExecution;
        public TasksRepository(ITaskManagementDatabaseSettings settings, IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(settings.DatabaseName);
            _tasks = database.GetCollection<Data.Models.Task>("Tasks");
            _tasksExecution = database.GetCollection<TaskExecution>("TasksExecution");
        }

        public TaskExecution CompleteTask(TaskExecution taskExecution)
        {
            taskExecution.TaskEndDate = DateTime.Now;
            _tasksExecution.ReplaceOne(t => t.Id == taskExecution.Id, taskExecution);

            return taskExecution;
        }

        public Data.Models.Task CreateTask(TaskVM taskVM)
        {
            Data.Models.Task task = new()
            {
                Name = $"{taskVM.ActionType} data from the {taskVM.TableName} table",
                ActionType = taskVM.ActionType,
                TableName = taskVM.TableName,
            };

            _tasks.InsertOne(task);

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

                _tasksExecution.InsertOne(taskExecution);

                return taskExecution;
            }
            else
                return null;
        }

        public List<Data.Models.Task> GetTasks() => _tasks.Find(task => true).ToList();

        public Data.Models.Task GetTask(Guid taskId)
        {
            var task = _tasks.Find(t => t.Id == taskId).SingleOrDefault();
            if (task is not null)
                return task;
            else
                return null;
        }

        public List<TaskExecution> GetTasksExecution() => _tasksExecution.Find(taskExecution => true).ToList();
    }
}
