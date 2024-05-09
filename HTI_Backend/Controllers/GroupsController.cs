using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.DTOs;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTI_Backend.Controllers
{
    public class GroupsController : ApiBaseController
    {
        private readonly IGenericRepository<Group> _groupRepo;
        private readonly IMapper _mapper;

        public GroupsController(IGenericRepository<Group> groupRepo, IMapper mapper)
        {
            _groupRepo = groupRepo;
            _mapper = mapper;
        }

        // Create a new group
        [HttpPost]
        [ProducesResponseType(typeof(GroupReturnDTO), 201)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> CreateGroup([FromBody] GroupCreateDTO groupCreateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));

            var group = _mapper.Map<GroupCreateDTO, Group>(groupCreateDTO);
            await _groupRepo.AddAsync(group);

            var groupReturnDTO = _mapper.Map<Group, GroupReturnDTO>(group);
            return CreatedAtAction(nameof(GetGroup), new { id = group.GroupId }, groupReturnDTO);
        }

        // Get a group by ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GroupReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetGroup(int id)
        {
            var groups = await _groupRepo.FindByCondition(g => g.GroupId == id);
            if (groups.Count == 0) return NotFound(new ApiResponse(404));

            var groupReturnDTO = _mapper.Map<Group, GroupReturnDTO>(groups.First());
            return Ok(groupReturnDTO);
        }

        // Get all groups
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GroupReturnDTO>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupRepo.FindByCondition(g => true);
            if (groups.Count == 0) return NotFound(new ApiResponse(404));

            var groupReturnDTOs = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupReturnDTO>>(groups);
            return Ok(groupReturnDTOs);
        }

        // Update a group
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GroupReturnDTO), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] GroupUpdateDTO groupUpdateDTO)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400));

            var groups = await _groupRepo.FindByCondition(g => g.GroupId == id);
            if (groups.Count == 0) return NotFound(new ApiResponse(404));

            var group = groups.First();
            _mapper.Map(groupUpdateDTO, group);
            await _groupRepo.UpdateAsync(group);

            var groupReturnDTO = _mapper.Map<Group, GroupReturnDTO>(group);
            return Ok(groupReturnDTO);
        }

        // Delete a group
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), 204)]
        [ProducesResponseType(typeof(ApiResponse), 404)]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var groups = await _groupRepo.FindByCondition(g => g.GroupId == id);
            if (groups.Count == 0) return NotFound(new ApiResponse(404));

            var group = groups.First();
            await _groupRepo.DeleteAsync(group);
            return NoContent();
        }
    }
}