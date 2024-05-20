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

        private readonly IGenericRepository<Group> _groupsRepo;
        private readonly IMapper _mapper;

        public GroupStudentsController(IMapper mapper, IGenericRepository<Group> groupsRepo)
        {
            
            _mapper = mapper;   
            _groupsRepo = groupsRepo;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<GroupStudentsReturnDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetAllStudentReqInGroups(int id)
        {
            var group = _groupsRepo.FindByCondition(w => w.GroupId == id, g => g.Include(g => g.Course).Include(w => w.Registrations).ThenInclude(w => w.Student).Include(w => w.Doctor).Include(w => w.TeachingAssistant)).Result.FirstOrDefault();
            if (group == null)
            {
                return NotFound(new ApiResponse(404));
            };
            var groupdto= _mapper.Map<GroupStudentsReturnDTO>(group);

            groupdto.Studs = _mapper.Map<List<student>>(group.Registrations.Select(e => e.Student).ToList());
            return Ok(groupdto);
            
        }
    }
    
}
