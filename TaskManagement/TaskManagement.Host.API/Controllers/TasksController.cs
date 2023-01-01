using Microsoft.AspNetCore.Mvc;

using TaskManagement.Data.VModels;
using TaskManagement.Services.Contract;

namespace TaskManagement.Host.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _tasksRepository;
        public TasksController(ITaskRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            return Ok(_tasksRepository.GetTasks());
        }

        [HttpPost("insert")]
        public IActionResult CreateTask([FromBody] TaskVM taskVM)
        {
            Data.Models.Task task = _tasksRepository.CreateTask(taskVM);
            return Ok(task);
        }
    }
}
