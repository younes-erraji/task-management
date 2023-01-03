using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using TaskManagement.Notifier;
using TaskManagement.Scheduler;
using TaskManagement.Services.Contract;

namespace TaskManagement.Host.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [EnableCors("AllowAllHeaders")]
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
      var task = _tasksExecutionRepository.GetTask(taskId);
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

    [HttpPost("{taskExecutionId}/complete")]
    public IActionResult CompleteTask(Guid taskExecutionId)
    {
      var taskExecution = _tasksExecutionRepository.GetTaskExecution(taskExecutionId);
      if (taskExecution is not null) {
        Thread.Sleep(7000);
        _tasksExecutionRepository.CompleteTask(taskExecution);

        return Ok(taskExecution);
      }
      else
      {
        return NotFound();
      }
    }
  }
}
