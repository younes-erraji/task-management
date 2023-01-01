using TaskManagement.Business.Workflow;
using TaskManagement.Data.Models;
using TaskManagement.Notifier;

namespace TaskManagement.Scheduler
{
    public class TaskScheduler: ITaskScheduler
    {
        private readonly ITaskManagementNotifier _taskManagementNotifier;
        private readonly ITaskWorkflow _taskWorkflow;

        public TaskScheduler(ITaskManagementNotifier taskManagementNotifier, ITaskWorkflow taskWorkflow)
        {
            _taskManagementNotifier = taskManagementNotifier;
            _taskWorkflow = taskWorkflow;
        }

        // public delegate Task TaskCreatedDelegate();
        // public event TaskCreatedDelegate TaskSchedulerCreated;

        // public delegate Task TaskCompletedDelegate();
        // public event TaskCompletedDelegate TaskSchedulerCompleted;

        void TaskSchedulerCreated(Data.Models.Task task)
        {
            // _taskManagementNotifier.StartingTask(task);
        }

        void TaskSchedulerCompleted(Data.Models.Task task)
        {
            // _taskManagementNotifier.EndingTask(task);
        }

        public TaskExecution Run(Data.Models.Task task)
        {
            TaskSchedulerCreated(task);
            TaskExecution taskExecution = _taskWorkflow.Execute(task);
            TaskSchedulerCompleted(task);

            return taskExecution;
        }
    }
}