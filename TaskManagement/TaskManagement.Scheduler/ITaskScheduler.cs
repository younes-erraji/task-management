using TaskManagement.Data.Models;

namespace TaskManagement.Scheduler
{
    public interface ITaskScheduler
    {
        TaskExecution Run(Data.Models.Task task);
    }
}
