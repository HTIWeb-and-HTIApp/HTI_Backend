using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HTI_Backend.Controllers
{
   
    public class ResultController : ApiBaseController
    {
        private readonly IGenericRepository<StudentCourseHistory> _resultRepo;
        private readonly IMapper _mapper;

        public ResultController(IGenericRepository<StudentCourseHistory> resultRepo ,IMapper mapper)
        {
            _resultRepo = resultRepo;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(ResultReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]

        public async Task<IActionResult> GetResultPassedOnYearAndSem([FromQuery] int Id, [FromQuery] int Year , [FromQuery] int Semester)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));
            var Result = await _resultRepo.FindByCondition(R => R.StudentId == Id && R.StudyYear == Year && R.Semester == Semester, R => R.Include(C => C.Course));
            if (Result == null) return NotFound(new ApiResponse(404));

            var MappedResult = _mapper.Map<IEnumerable<Course>, IEnumerable<coursesdTO>>(Result.Select(w => w.Course));




            return Ok(MappedResult);
        }
    }
}
