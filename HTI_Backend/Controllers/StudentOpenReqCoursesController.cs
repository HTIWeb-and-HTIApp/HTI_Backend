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
   
    public class StudentOpenReqCoursesController : ApiBaseController
    {
        private readonly IGenericRepository<StudentCourseHistory> _couRepo;
        private readonly IMapper _mapper;

        public StudentOpenReqCoursesController(IGenericRepository<StudentCourseHistory> couRepo, IMapper mapper)
        {
            _couRepo = couRepo;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StudentOpenReqCoursesReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> StudentOpenReqCourses(int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));
            var regs = await _couRepo.FindByCondition(s => s.StudentId == id &&  s.Status == false );
            var mapped = _mapper.Map<IEnumerable<StudentCourseHistory>, IEnumerable<StudentOpenReqCoursesReturnDTO>>(regs);
            var obj = new StudentOpenReqCoursesDTO()
            {
                CourseCount = mapped.Count(),
                Courses = mapped
            };

            return Ok(obj);
        }
        class StudentOpenReqCoursesDTO
        {
            public int CourseCount { get; set; }
            public IEnumerable<StudentOpenReqCoursesReturnDTO>  Courses { get; set; }
        }
    }
}
