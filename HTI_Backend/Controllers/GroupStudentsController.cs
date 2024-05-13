using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using Group = HTI.Core.Entities.Group;

namespace HTI_Backend.Controllers
{

    public class GroupStudentsController : ApiBaseController
    {

        private readonly IGenericRepository<Registration> _groupRepo;
        private readonly IMapper _mapper;

        public GroupStudentsController(IGenericRepository<Registration> groupRepo, IMapper mapper)
        {
            _groupRepo = groupRepo;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<GroupStudentsReturnDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetAllStudentReqInGroups(int id)
        {
            var GroupStudents = await _groupRepo.FindByCondition( S => S.GroupId == id, S => S.Include(S => S.Group).ThenInclude(S => S.Course).Include(S => S.Group).ThenInclude(S => S.Doctor).Include(S => S.Group).ThenInclude(S => S.TeachingAssistant).Include(S=> S.Student), S => S.OrderBy(S => S.GroupId));
            if (GroupStudents.Count == 0) return NotFound(new ApiResponse(404));

            var ReturnGroupStudents = _mapper.Map<IEnumerable<Registration>, IEnumerable<GroupStudentsReturnDTO>>(GroupStudents);
            var groupedStudents = ReturnGroupStudents.GroupBy(dto => dto.GroupId);

            var response = groupedStudents.Select(group => new
            {
                GroupId = group.Key,
                CourseId = group.First().CourseId,
                CourseCode = group.First().CourseCode,
                CourseName = group.First().CourseName,
                DoctorName = group.First().DoctorName,
                TeachingAssistantName = group.First().TeachingAssistantName,
                Students = group.Select(dto => new
                {
                    StudentId = dto.StudentId,
                    StudentName = dto.StudentName
                })
            });

            return Ok(ReturnGroupStudents);

            //return Ok(ReturnGroupStudents);
        }
    }
    
}
