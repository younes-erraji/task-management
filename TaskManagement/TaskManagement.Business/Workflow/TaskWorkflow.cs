using System;

using TaskManagement.Data.Models;
using TaskManagement.Business.Services;

namespace TaskManagement.Business.Workflow
{
    public class TaskWorkflow : ITaskWorkflow
    {
        private readonly ITaskManagementService _taskManagementService;
        public TaskWorkflow(ITaskManagementService taskManagementService)
        {
            _taskManagementService = taskManagementService;
        }

        public TaskExecution Execute(Data.Models.Task task)
        {
            if (task.ActionType.Equals("Purge", StringComparison.OrdinalIgnoreCase))
            {
                return _taskManagementService.DeleteAllDataTask(task);
            }
            else
            {
                return _taskManagementService.AddRandomDataTask(task);
            }            
        }
    }
}
