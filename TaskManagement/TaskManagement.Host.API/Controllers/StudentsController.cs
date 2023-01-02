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
  public class StudentsController : ControllerBase
  {
    private readonly IStudentsRepository _studentsRepository;
    public StudentsController(IStudentsRepository studentsRepository)
    {
      _studentsRepository = studentsRepository;
    }

    [HttpGet]
    public IActionResult GetStudents()
    {
      List<Student> students = _studentsRepository.GetStudents();
      return Ok(students);
    }

    [HttpPut("{studentId}/edit")]
    public IActionResult EditStudent(Guid studentId, [FromBody] StudentVM studentVM)
    {
      Student student = _studentsRepository.EditStudent(studentVM, studentId);

      if (student is not null)
        return Ok(student);
      else
        return NotFound();
    }

    [HttpDelete("{studentId}/delete")]
    public IActionResult DeleteStudent(Guid studentId)
    {
      int deleted = _studentsRepository.DeleteStudent(studentId);

      if (deleted == 1)
        return Ok();
      else
        return NotFound();
    }

    [HttpPost("insert")]
    public IActionResult CreateStudent([FromBody] StudentVM studentVM)
    {
      Student student = _studentsRepository.CreateStudent(studentVM);
      return Ok(student);
    }

    [HttpGet("{studentId}/get")]
    public IActionResult GetStudents(Guid studentId)
    {
      Student student = _studentsRepository.GetStudent(studentId);
      if (student is not null)
        return Ok(student);
      else
        return NotFound();
    }
  }
}
