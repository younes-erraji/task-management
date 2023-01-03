using Microsoft.AspNetCore.SignalR;

using TaskManagement.Services.Contract;

namespace TaskManagement.Notifier
{
  public class TaskManagementNotifier : Hub, ITaskManagementNotifier
  {
    private readonly ITaskRepository _taskRepository;

    public TaskManagementNotifier(ITaskRepository taskRepository)
    {
      _taskRepository = taskRepository;
    }

    public async Task StartingTask(Guid taskId)
    {
      var task = _taskRepository.GetTask(taskId);
      if (task is not null)
      {
        if (task.TableName.Equals("students", StringComparison.OrdinalIgnoreCase))
          await Clients.All.SendAsync("StudentsTaskStarted", task.ActionType);
        else
          await Clients.All.SendAsync("TeachersTaskStarted", task.ActionType);
      }
    }

    public async Task EndingTask(Guid taskId)
    {
      var task = _taskRepository.GetTask(taskId);
      if (task is not null)
      {
        if (task.TableName.Equals("students", StringComparison.OrdinalIgnoreCase))
          await Clients.All.SendAsync("StudentsTaskEnd", task.ActionType);
        else
          await Clients.All.SendAsync("TeachersTaskEnd", task.ActionType);
      }
    }
  }
}
