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

    public class TeachingAssistantController : ApiBaseController
    {
        private readonly IGenericRepository<TeachingAssistant> _tARepo;
        private readonly IMapper _mapper;

        public TeachingAssistantController(IGenericRepository<TeachingAssistant> TARepo, IMapper mapper)
        {
            _tARepo = TARepo;
            _mapper = mapper;
        }
        [HttpGet("InTrem/{id}")]
        [ProducesResponseType(typeof(TACoursesReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetTACoursesInTerm(int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));
            var TACourses = _tARepo.FindByCondition(S => S.TeachingAssistantId == id, S => S.Include(C => C.Groups).ThenInclude(T => T.Course)).Result.FirstOrDefault();
            if (TACourses == null) return NotFound(new ApiResponse(404));
            TACourses.Groups = TACourses.Groups.Where(w => w.IsOpen == true).ToList();
            var mappedTACourses = _mapper.Map<TACoursesReturnDto>(TACourses);
            mappedTACourses.courses = _mapper.Map<IEnumerable<course>>(TACourses.Groups.Select(C => C.Course).Distinct());

            return Ok(mappedTACourses);
        }



        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TACoursesReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetTACourses(int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));
            var TACourses = _tARepo.FindByCondition(S => S.TeachingAssistantId == id, S => S.Include(C => C.Groups).ThenInclude(T => T.Course)).Result.FirstOrDefault();
            if (TACourses == null) return NotFound(new ApiResponse(404));
            var mappedTACourses = _mapper.Map<TACoursesReturnDto>(TACourses);
            mappedTACourses.courses = _mapper.Map<IEnumerable<course>>(TACourses.Groups.Select(C => C.Course).Distinct());

            return Ok(mappedTACourses);
        }
    }
}
