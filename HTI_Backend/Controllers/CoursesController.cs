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

    public class CoursesController : ApiBaseController
    {
        private readonly IGenericRepository<Course> _corseRepo;
        private readonly IMapper _mapper;

        public CoursesController(IGenericRepository<Course> CorseRepo, IMapper mapper)
        {
            _corseRepo = CorseRepo;
            _mapper = mapper;
        }


        //Get All Courses
        [HttpGet]
        [ProducesResponseType(typeof(AllCoursesReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]

        public async Task<IActionResult> GetAllCourses()
        {
            var Courses = await _corseRepo.FindByCondition(null, D => D.Include(C => C.Department), C => C.OrderBy(c => c.StudyYear));
            

            if (Courses is null) return NotFound(new ApiResponse(404));
            var mappedCourses = _mapper.Map<IEnumerable<Course>, IEnumerable<AllCoursesReturnDTO>>(Courses);

            return Ok(mappedCourses);
        }




        //Get Courses based on semester

        [HttpGet("{Semester}")]
        [ProducesResponseType(typeof(AllCoursesReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetCoursesBySemester(int Semester)
        {
            var courses = await _corseRepo.FindByCondition(C => C.Semester == Semester, D => D.Include(C => C.Department), C => C.OrderBy(c => c.StudyYear));
            if (courses is null) return NotFound(new ApiResponse(404));

            var mappedCourses = _mapper.Map<IEnumerable<Course>, IEnumerable<AllCoursesReturnDTO>>(courses);


            return Ok(mappedCourses);
        }



        [HttpGet("{StudyYear}/{Semester}")]
        [ProducesResponseType(typeof(AllCoursesReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetCoursesByStudyYearAndSemester(int StudyYear, int Semester)
        {
            var courses = await _corseRepo.FindByCondition(C => C.StudyYear == StudyYear && C.Semester == Semester, D => D.Include(C => C.Department), C => C.OrderBy(c => c.StudyYear));
            if (courses is null) return NotFound(new ApiResponse(404));

            var mappedCourses = _mapper.Map<IEnumerable<Course>, IEnumerable<AllCoursesReturnDTO>>(courses);

            return Ok(mappedCourses);
        }

        [HttpGet("Type/{Type}")]
        [ProducesResponseType(typeof(AllCoursesReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetCoursesByType(string Type)
        {
            var courses = await _corseRepo.FindByCondition(C => C.Type == Type, D => D.Include(C => C.Department), C => C.OrderBy(c => c.StudyYear)) ;

            if (courses is null) return NotFound(new ApiResponse(404));


            var mappedCourses = _mapper.Map<IEnumerable<Course>, IEnumerable<AllCoursesReturnDTO>>(courses);


            return Ok(mappedCourses);
        }
    }
}
