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
        CompleteTask(taskExecution);
        return Ok(taskExecution);
      }
      else
      {
        return NotFound();
      }
    }

    [NonAction]
    public Task CompleteTask(Data.Models.TaskExecution task)
    {
      return Task.Run(() =>
      {
        Thread.Sleep(10000);
        _tasksExecutionRepository.CompleteTask(task);
      });
    }
  }
}
