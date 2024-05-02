using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace HTI_Backend.Controllers
{

    public class StudentController : ApiBaseController
    {
        private readonly IGenericRepository<Student> _studentRepo;
        private readonly IMapper _mapper;

        public StudentController(IGenericRepository<Student> StudentRepo ,IMapper mapper)
        {
            _studentRepo = StudentRepo;
            _mapper = mapper;
        }
       
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(StudentToReturnDTO),200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetStudentById(int Id)
        {
            var students =  await _studentRepo.FindByCondition(S => S.StudentId == Id, D => D.Include(S => S.Department));
            var str = students.FirstOrDefault();
            if (str is null) return NotFound(new ApiResponse(404)); 
            var MappeedStudent = _mapper.Map<Student, StudentToReturnDTO>(str);
            return Ok(MappeedStudent);
        }

        [HttpGet]
        [ProducesResponseType(typeof(AllStudentReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]

        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentRepo.FindByCondition(null, D => D.Include(S => S.Department));
            
            if (students is null) return NotFound(new ApiResponse(404));
            var mappedStudents = _mapper.Map<IEnumerable<Student>, IEnumerable<AllStudentReturnDTO>>(students);

            return Ok(mappedStudents);
        }
    }
}
    