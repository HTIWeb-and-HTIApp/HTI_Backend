using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTI_Backend.Controllers
{
    
    public class StudentLastTermController : ApiBaseController
    {
        private readonly IGenericRepository<Student> _studentRepo;
        private readonly IMapper _mapper;

        public StudentLastTermController(IGenericRepository<Student> StudentRepo, IMapper mapper)
        {
            _studentRepo = StudentRepo;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(typeof(LastTermReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> LastTermReport()
        {

            var students = await _studentRepo.FindByCondition(S => S.Credits > 137);

            if (students is null) return NotFound(new ApiResponse(404));
            var mappedStudents = _mapper.Map<IEnumerable<Student>, IEnumerable<LastTermReturnDTO>>(students);

            var report = new LastTermDto
            {
                StudentCount = mappedStudents.Count(),
                Students = mappedStudents
            };

            return Ok(report);
        }

    }
    public class LastTermDto
    {
        public int StudentCount { get; set; }
        public IEnumerable<LastTermReturnDTO> Students { get; set; }
    }
}

