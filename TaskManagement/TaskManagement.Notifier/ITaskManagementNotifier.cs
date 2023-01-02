namespace TaskManagement.Notifier
{
    public interface ITaskManagementNotifier
    {
        Task StartingTask(Guid taskId);
        Task EndingTask(Guid taskId);
    }
}
