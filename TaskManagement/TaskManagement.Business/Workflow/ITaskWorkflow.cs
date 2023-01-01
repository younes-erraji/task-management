using TaskManagement.Data.Models;

namespace TaskManagement.Business.Workflow
{
    public interface ITaskWorkflow
    {
        TaskExecution Execute(Data.Models.Task task);
    }
}
