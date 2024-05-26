using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using HTI.Core.RepositoriesContract;
using Microsoft.EntityFrameworkCore;
using HTI.Core.Entities;
using AutoMapper;
using System.Linq;


namespace HTI_Backend.Controllers
{

    

public class ScheduleController : ApiBaseController
    {
        private readonly IGenericRepository<Registration> _groupsRepo;
        private readonly IMapper _mapper;

        public ScheduleController(IMapper mapper, IGenericRepository<Registration> groupsRepo)
        {
            _mapper = mapper;
            _groupsRepo = groupsRepo;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ScheduleReturnDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetScheduleByDay([FromQuery] string day, [FromQuery] int Id)
        {
            var schedules = await _groupsRepo.FindByCondition(G => (G.Group.SectionDay == day || G.Group.LectureDay == day) && G.Student.StudentId == Id, C => C.Include(S => S.Student).Include(S => S.Group).ThenInclude(S => S.Course));

            var openSchedules = schedules.Where(s => s.IsOpen == true).ToList();
            if (openSchedules == null || !schedules.Any())
            {
                return NotFound(new ApiResponse(404));
            }

            var scheduleDTOs = openSchedules.Select(g =>
            {
                var dto = _mapper.Map<ScheduleReturnDTO>(g);
                if (g.Group.SectionDay == day)
                {
                    dto.CourseType = "Section";
                    dto.Room = g.Group.SectionRoom;
                    dto.StartTime = g.Group.SectionTime;
                    dto.EndTime= slots[TimeToPeriods(g.Group.SectionTime, section_duration_periods).Last()];
                }
                else
                {
                    dto.CourseType = "Lecture";
                    dto.Room = g.Group.LectureRoom;
                    dto.StartTime = g.Group.LectureTime;
                    dto.EndTime = slots[TimeToPeriods(g.Group.LectureTime, lecture_duration_periods).Last()];
                }
                return dto;
            }).OrderBy(dto => dto.EndTime).ToList();

            return Ok(scheduleDTOs);
        }

        private List<string> slots = new List<string> { "9:00", "9:45", "10:40", "11:25", "12:20", "13:05", "14:00", "14:45", "15:30" };
        private int lecture_duration_periods = 4;
        private int section_duration_periods = 3;

        private List<int> TimeToPeriods(string start_time, int periods)
        {
            int start_index = slots.IndexOf(start_time);
            return Enumerable.Range(start_index, periods).ToList();
        }
    }
    

}


