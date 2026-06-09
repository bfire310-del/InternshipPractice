using InternshipPractice.Application.Queries.GetCompaniesForCareer;
using InternshipPractice.Application.Queries.GetStudentsByCareerCenter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CareerController : BaseController
{
    [Authorize]
    [HttpGet("students-for-career")]
    public async Task<IActionResult> GetStudentsByCareerCenter()
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;

        if (userIdClaim is null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);
        var result = await Mediator.Send(new GetStudentsByCareerCenterQuery(userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("companies-for-career")]
    public async Task<IActionResult> GetCompaniesForCareer()
    {
        var result = await Mediator.Send(new GetCompaniesForCareerQuery());

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
