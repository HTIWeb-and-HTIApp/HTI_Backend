using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI_Backend.Controllers;
using HTI_Backend.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

public class graduation_Project_FormController : ApiBaseController
{
    private readonly IGraduationRepository _trainingrepo;

    public graduation_Project_FormController(IGraduationRepository trainingrepo)
    {
        _trainingrepo = trainingrepo;
    }

    [HttpPost]
    public async Task<IActionResult> GraduationProject([FromBody] Team registration)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse(404));
        }

        await _trainingrepo.AddGraduationAsync(registration);

        return Ok("Registration successful!");
    }
}
