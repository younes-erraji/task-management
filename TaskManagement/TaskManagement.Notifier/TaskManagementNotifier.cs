using Microsoft.AspNetCore.SignalR;

namespace TaskManagement.Notifier
{
    public class TaskManagementNotifier : Hub, ITaskManagementNotifier
    {
        public async Task StartingTask(Data.Models.Task task)
        {
            await Clients.All.SendAsync("StartTask", task);
        }

        public async Task EndingTask(Data.Models.Task task)
        {
            await Clients.All.SendAsync("EndTask", task);
        }
    }
}
