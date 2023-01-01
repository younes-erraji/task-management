using TaskManagement.Data.VModels;
using TaskManagement.Data.Models;

namespace TaskManagement.Services.Contract
{
    public interface ITaskRepository
    {
        List<Data.Models.Task> GetTasks();
        Data.Models.Task GetTask(Guid taskId);
        List<TaskExecution> GetTasksExecution();

        Data.Models.Task CreateTask(TaskVM taskVM);
        TaskExecution ExecuteTask(Data.Models.Task task);

        TaskExecution CompleteTask(TaskExecution task);
    }
}
