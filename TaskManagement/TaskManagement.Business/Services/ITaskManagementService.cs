using TaskManagement.Data.Models;

namespace TaskManagement.Business.Services
{
    public interface ITaskManagementService
    {
        TaskExecution DeleteAllDataTask(Data.Models.Task task);
        TaskExecution AddRandomDataTask(Data.Models.Task task);
    }
}
