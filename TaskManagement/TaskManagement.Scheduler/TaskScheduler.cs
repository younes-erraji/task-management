using TaskManagement.Business.Workflow;
using TaskManagement.Data.Models;

namespace TaskManagement.Scheduler
{
    public class TaskScheduler: ITaskScheduler
    {
        private readonly ITaskWorkflow _taskWorkflow;

        public TaskScheduler(ITaskWorkflow taskWorkflow)
        {
            _taskWorkflow = taskWorkflow;
        }

        public TaskExecution Run(Data.Models.Task task)
        {
            TaskExecution taskExecution = _taskWorkflow.Execute(task);

            return taskExecution;
        }
    }
}