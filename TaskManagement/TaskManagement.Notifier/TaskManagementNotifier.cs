using Microsoft.AspNetCore.SignalR;

using TaskManagement.Services.Contract;

namespace TaskManagement.Notifier
{
    public class TaskManagementNotifier : Hub, ITaskManagementNotifier
    {
        private readonly ITaskRepository _taskRepository;

        public TaskManagementNotifier(ITaskRepository taskRepository) {
            _taskRepository = taskRepository;
        }

        public async Task StartingTask(Guid taskId)
        {
            var task = _taskRepository.GetTask(taskId);
            if (task is not null)
            {
                await Clients.All.SendAsync("TaskStarted", task.Id, task.Name, task.TableName, task.ActionType);
            }
            else
            {
                await Clients.All.SendAsync("TaskStarted", "Unknown Task");
            }
        }

        public async Task EndingTask(Guid taskId)
        {
            var task = _taskRepository.GetTask(taskId);
            if (task is not null)
            {
                await Clients.All.SendAsync("TaskEnd", task.Id, task.Name, task.TableName, task.ActionType);
            }
            else
            {
                await Clients.All.SendAsync("TaskEnd", "Unknown Task");
            }
        }
    }
}
