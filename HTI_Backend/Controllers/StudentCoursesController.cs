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

    public class StudentCoursesController : ApiBaseController
    {
        private readonly IGenericRepository<Registration> _regRepo;
        private readonly IMapper _mapper;

        public StudentCoursesController(IGenericRepository<Registration> regRepo, IMapper mapper)
        {
            _regRepo = regRepo;
            _mapper = mapper;
        }





        [HttpGet("{id}")]
        [ProducesResponseType(typeof(StudentCoursesRetuenDTOs), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetReg(int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));
            var regs = await _regRepo.FindByCondition(g => g.StudentId == id  , D => D.Include(S => S.Group).ThenInclude(T => T.Course));
            if (regs.Count == 0) return NotFound(new ApiResponse(404));
            var mappedRegs = _mapper.Map<IEnumerable<Registration>, IEnumerable<StudentCoursesRetuenDTOs>>(regs);

            return Ok(mappedRegs);
        }
        [HttpGet("MyCoursesinThisTerm/{id}")]
        [ProducesResponseType(typeof(StudentCoursesRetuenDTOs), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetReqInThisTerm(int id)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));
            var regs = await _regRepo.FindByCondition(g => g.StudentId == id && g.IsOpen == true, D => D.Include(S => S.Group).ThenInclude(T => T.Course));
            if (regs.Count == 0) return NotFound(new ApiResponse(404));
            var mappedRegs = _mapper.Map<IEnumerable<Registration>, IEnumerable<StudentCoursesRetuenDTOs>>(regs);

            return Ok(mappedRegs);
        }


    }
}
