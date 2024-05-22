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
        public async Task<IActionResult> GetallStudentsLastTermCourses()
        {
            var StudentsLastTermCourses = await _courseRepo.FindByCondition(S => S.Student.Credits >= 137 && S.Status == 1, T => T.Include(T => T.Course).Include(T => T.Student));
            if (StudentsLastTermCourses.Count == 0) return NotFound(new ApiResponse(404));

            var ReturnStudentsLastTermCourses = _mapper.Map<StudentsLastTermCoursesDTOs>(StudentsLastTermCourses);

             ReturnStudentsLastTermCourses.Studss = _mapper.Map<IEnumerable<student>>(StudentsLastTermCourses.Select(e => e.Student).ToList());

            return Ok(ReturnStudentsLastTermCourses);

        }
    }
}