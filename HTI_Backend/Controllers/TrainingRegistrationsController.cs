using System.Text.Json;
using AutoMapper;
using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HTI_Backend.Controllers
{

    public class TrainingRegistrationsController : ApiBaseController
    {
        private readonly ItrainningResiteration _trainingrepo;

        public TrainingRegistrationsController(ItrainningResiteration trainingrepo )
        {
            _trainingrepo = trainingrepo;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterTraining([FromBody] TrainingRegistration registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse(404));
            }

            await _trainingrepo.AddTrainingRegistrationAsync(registration);

            return Ok("Registration successful!");
        }
    }
}
