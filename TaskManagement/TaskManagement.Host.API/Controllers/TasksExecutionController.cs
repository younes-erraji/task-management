using Microsoft.AspNetCore.Mvc;

using TaskManagement.Scheduler;
using TaskManagement.Services.Contract;

namespace TaskManagement.Host.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksExecutionController : ControllerBase
    {
        private readonly ITaskScheduler _tasksScheduler;
        private readonly ITaskRepository _tasksExecutionRepository;
        public TasksExecutionController(ITaskRepository tasksExecutionRepository, ITaskScheduler tasksScheduler)
        {
            _tasksScheduler = tasksScheduler;
            _tasksExecutionRepository = tasksExecutionRepository;
        }

        [HttpGet]
        public IActionResult GetTasksExecution()
        {
            return Ok(_tasksExecutionRepository.GetTasksExecution());
        }

        [HttpPost("{taskId}/execute")]
        public IActionResult ExecuteTask(Guid taskId)
        {
            Data.Models.Task task = _tasksExecutionRepository.GetTask(taskId);
            if (task is not null)
            {
                var taskExecution = _tasksScheduler.Run(task);
                return Ok(taskExecution);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
