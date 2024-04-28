using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace HTI_Backend.Controllers
{

    public class StudentController : ApiBaseController
    {
        private readonly IGenericRepository<Student> _studentRepo;

        public StudentController(IGenericRepository<Student> StudentRepo)
        {
            _studentRepo = StudentRepo;
        }
       
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetStudentById(int Id)
        {
            var students =  await _studentRepo.FindByCondition(S => S.StudentId == Id, D => D.Include(S => S.Department));
            var str = students.FirstOrDefault();
            return Ok(str);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentRepo.FindByCondition(null, D => D.Include(S => S.Department).Include(S=> S.Registrations).ThenInclude(S => S.Group));
            var str = students.FirstOrDefault();
            return Ok(str);
        }
    }
}
