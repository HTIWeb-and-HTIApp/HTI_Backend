using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HTI_Backend.Controllers
{
    public class StudentsLastTermCourses : ApiBaseController
    {
        private readonly IGenericRepository<StudentCourseHistory> _courseRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Course> _allcourseRepo;

        public StudentsLastTermCourses(IGenericRepository<StudentCourseHistory> courseRepo, IMapper mapper, IGenericRepository<Course> allcourseRepo)
        {
            _courseRepo = courseRepo;
            _mapper = mapper;
            _allcourseRepo = allcourseRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<StudentsLastTermCoursesDTOs>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetallStudentsLastTermCourses()
        {
            var allCourses = await _allcourseRepo.GetAllAsync();

            var studentCourseHistories = await _courseRepo.FindByCondition(
                (s => s.Student.Credits >= 137 && s.Status == true && s.StudentId == 42020151),
                t => t.Include(t => t.Course).Include(t => t.Student),
                C => C.OrderBy(c => c.StudentId));

            if (!studentCourseHistories.Any())
                return NotFound(new ApiResponse(404));


            var remainingCoursesPerStudent = studentCourseHistories
                .GroupBy(sch => sch.StudentId)
                .Select(group => new
                {
                    StudentId = group.Key,
                    RemainingCourses = allCourses.Where(c => !group.Select(sch => sch.Course.CourseCode).Contains(c.CourseCode))
                });

            var distinctCourses = remainingCoursesPerStudent
                .SelectMany(student => student.RemainingCourses)
                .GroupBy(course => course.CourseCode)
                .Select(group => new StudentsLastTermCoursesDTOs
                {
                    CourseCode = group.Key,
                    CourseName = group.First().Name,
                    StudentCount = group.Count()
                });

            return Ok(distinctCourses);
        }
    }
}
