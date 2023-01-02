using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using TaskManagement.Services.Contract;
using TaskManagement.Data.VModels;
using TaskManagement.Data.Models;

namespace TaskManagement.Host.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [EnableCors("AllowAllHeaders")]
  public class TeachersController : ControllerBase
  {
    private readonly ITeachersRepository _teachersRepository;
    public TeachersController(ITeachersRepository teachersRepository)
    {
      _teachersRepository = teachersRepository;
    }

    [HttpGet]
    public IActionResult GetTeachers()
    {
      List<Teacher> teachers = _teachersRepository.GetTeachers();
      return Ok(teachers);
    }

    [HttpPut("{teacherId}/edit")]
    public IActionResult EditTeacher(Guid teacherId, [FromBody] TeacherVM teacherVM)
    {
      Teacher teacher = _teachersRepository.EditTeacher(teacherVM, teacherId);

      if (teacher is not null)
        return Ok(teacher);
      else
        return NotFound();
    }

    [HttpDelete("{teacherId}/delete")]
    public IActionResult DeleteTeacher(Guid teacherId)
    {
      int deleted = _teachersRepository.DeleteTeacher(teacherId);

      if (deleted == 1)
        return Ok();
      else
        return NotFound();
    }

    [HttpPost("insert")]
    public IActionResult CreateTeacher([FromBody] TeacherVM teacherVM)
    {
      Teacher teacher = _teachersRepository.CreateTeacher(teacherVM);
      return Ok(teacher);
    }

    [HttpGet("{teacherId}/get")]
    public IActionResult GetTeacher(Guid teacherId)
    {
      Teacher teacher = _teachersRepository.GetTeacher(teacherId);
      if (teacher is not null)
        return Ok(teacher);
      else
        return NotFound();
    }
  }
}
