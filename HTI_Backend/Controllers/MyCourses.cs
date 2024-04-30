using HTI.Core.RepositoriesContract;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTI_Backend.Controllers
{
    
    public class MyCourses : ApiBaseController
    {
        private readonly IRegistrationRepository _registrationRepository;

        public MyCourses(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }
        [HttpGet("{studentId}")]
        public async Task<IActionResult> GetGroupIdsForStudent(int studentId)
        {
            var registrations = await _registrationRepository.GetRegistrationsByStudentId(studentId);
            if (registrations is null) return NotFound(new ApiResponse(404));

            var groupIds = registrations.Select(r => r.GroupId).Distinct();
            var groups = await _registrationRepository.GetGroupsByIds(groupIds);

            return Ok(groups);

        }

    }
}
