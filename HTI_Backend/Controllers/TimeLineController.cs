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

    public class TimeLineController : ApiBaseController
    {
        private readonly IGenericRepository<TimeLine> _timeLineRepo;
        private readonly IMapper _mapper;

        public TimeLineController(IGenericRepository<TimeLine> timeLineRepo, IMapper mapper)
        {
            _timeLineRepo = timeLineRepo;
            _mapper = mapper;
        }
        // Create a new timeline

        [HttpPost]
        [ProducesResponseType(typeof(TimeLineReturnDTO), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> CreateTimeLine([FromBody] TimeLineCreateDTO timeLineCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));

            var timeLine = _mapper.Map<TimeLineCreateDTO, TimeLine>(timeLineCreateDTO);
            await _timeLineRepo.AddAsync(timeLine);
            return Ok();
        }
        // Create multiple timelines


        [HttpPost("bulk")]
        [ProducesResponseType(typeof(IEnumerable<TimeLineReturnDTO>), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> CreateTimeLines([FromBody] IEnumerable<TimeLineCreateDTO> timeLineCreateDTOs)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));

            var timeLines = _mapper.Map<IEnumerable<TimeLineCreateDTO>, IEnumerable<TimeLine>>(timeLineCreateDTOs);
            await _timeLineRepo.AddRangeAsync(timeLines);

            return Ok();

        }
        // Get a timeline by ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TimeLineReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetTimeLine(int id)
        {
            var timeLines = await _timeLineRepo.FindByCondition(t => t.TimeLineId == id , S => S.Include(T => T.Group).ThenInclude(T => T.Course));
            if (!timeLines.Any()) return NotFound(new ApiResponse(404));

            var timeLineReturnDTO = _mapper.Map<TimeLine, TimeLineReturnDTO>(timeLines.First());
            return Ok(timeLineReturnDTO);
        }
        // Get all timelines
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TimeLineReturnDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetAllTimeLines()
        {
            var timeLines = await _timeLineRepo.FindByCondition( null , S => S.Include(T => T.Group) );
            if (!timeLines.Any()) return NotFound(new ApiResponse(404));

            var timeLineReturnDTOs = _mapper.Map<IEnumerable<TimeLine>, IEnumerable<TimeLineReturnDTO>>(timeLines);
            return Ok(timeLineReturnDTOs);
        }
        // Update a timeline
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TimeLineReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> UpdateTimeLine(int id, [FromBody] TimeLineUpdateDTO timeLineUpdateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));

            var timeLines = await _timeLineRepo.FindByCondition(t => t.TimeLineId == id , S => S.Include(T => T.Group).ThenInclude(T => T.Course));
            if (!timeLines.Any()) return NotFound(new ApiResponse(404));

            var timeLine = timeLines.First();
            _mapper.Map(timeLineUpdateDTO, timeLine);
            await _timeLineRepo.UpdateAsync(timeLine);

            var timeLineReturnDTO = _mapper.Map<TimeLine, TimeLineReturnDTO>(timeLine);
            return Ok(timeLineReturnDTO);
        }   
        // Delete a timeline
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), 204)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> DeleteTimeLine(int id)
        {
            var timeLines = await _timeLineRepo.FindByCondition(t => t.TimeLineId == id);
            if (!timeLines.Any()) return NotFound(new ApiResponse(404));

            var timeLine = timeLines.First();
            await _timeLineRepo.DeleteAsync(timeLine);
            return NoContent();
        }

    }
}
