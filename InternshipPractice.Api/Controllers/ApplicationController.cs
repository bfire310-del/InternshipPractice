using System.Security.Claims;
using InternshipPractice.Api.Requests;
using InternshipPractice.Application.Queries.CreateApplication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ApplicationController : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> CreateApplication(CreateApplicationRequest request)
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new CreateApplicationCommand(
            request.VacancyId,
            userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result);
    }
}
