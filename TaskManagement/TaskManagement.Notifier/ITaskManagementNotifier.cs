namespace TaskManagement.Notifier
{
    public interface ITaskManagementNotifier
    {
        Task StartingTask(Data.Models.Task task);
        Task EndingTask(Data.Models.Task task);
    }
}
