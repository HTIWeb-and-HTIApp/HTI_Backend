using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI.Repository.Data;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HTI_Backend.Controllers
{

    public class DoctorCoursesController : ApiBaseController
    {
        private readonly IGenericRepository<Doctor> _doctorRepo;
        private readonly IMapper _mapper;

        public DoctorCoursesController(IGenericRepository<Doctor> doctorRepo, IMapper mapper)
        {
            _doctorRepo = doctorRepo;
            _mapper = mapper;
        }
        [HttpGet("InTrem/{id}")]
        [ProducesResponseType(typeof(DoctorCoursesReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetDocCoursesInTerm(int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));
            var DoctorCourses = _doctorRepo.FindByCondition(S => S.DoctorId == id, S => S.Include(C => C.Groups).ThenInclude(T => T.Course)).Result.FirstOrDefault();
            DoctorCourses.Groups = DoctorCourses.Groups.Where(w => w.IsOpen).ToList();
            if (DoctorCourses == null) return NotFound(new ApiResponse(404));
            var mappedDoctorCourses = _mapper.Map<DoctorCoursesReturnDto>(DoctorCourses);
            mappedDoctorCourses.courses = _mapper.Map<IEnumerable<course>>(DoctorCourses.Groups.Select(C => C.Course).Distinct());

            return Ok(mappedDoctorCourses);
        }
        
        
        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DoctorCoursesReturnDto), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetDocCourses(int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));
            var DoctorCourses = _doctorRepo.FindByCondition(S => S.DoctorId == id, S => S.Include(C => C.Groups).ThenInclude(T => T.Course)).Result.FirstOrDefault();
            if (DoctorCourses == null) return NotFound(new ApiResponse(404));
            var mappedDoctorCourses = _mapper.Map<DoctorCoursesReturnDto>(DoctorCourses);
            mappedDoctorCourses.courses = _mapper.Map<IEnumerable<course>>(DoctorCourses.Groups.Select(C => C.Course).Distinct());

            return Ok(mappedDoctorCourses);
        }
    }
}
