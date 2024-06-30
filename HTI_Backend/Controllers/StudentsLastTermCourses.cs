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
    public class StudentsLastTermCourses : ApiBaseController
    {
        private readonly IGenericRepository<StudentCourseHistory> _courseRepo;
        private readonly IMapper _mapper;

        public StudentsLastTermCourses(IGenericRepository<StudentCourseHistory> courseRepo, IMapper mapper)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StudentsLastTermCoursesDTOs>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetAllStudentsLastTermCourses()
        {
            var studentsLastTermCourses = await _courseRepo.FindByCondition(
                s => s.Student.Credits >= 137 && s.Status == 1,
                t => t.Include(t => t.Course).Include(t => t.Student)
            );

            if (!studentsLastTermCourses.Any())
                return NotFound(new ApiResponse(404));

            var returnStudentsLastTermCourses = _mapper.Map<IEnumerable<StudentsLastTermCoursesDTOs>>(studentsLastTermCourses);

            return Ok(returnStudentsLastTermCourses);
        }


    }
}