using System.Security.Claims;
using InternshipPractice.Application.Queries.GetActiveContractsCount;
using InternshipPractice.Application.Queries.GetCompletedContractsCount;
using InternshipPractice.Application.Queries.GetContractsByUserId;
using InternshipPractice.Application.Queries.GetContractsCount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContractController : BaseController
{
    
    [HttpGet("by-user")]
    public async Task<IActionResult> GetContractsByUserId(
        [FromQuery] string lang = "ru",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 5)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(
            new GetContractsByUserIdQuery(userId, lang, page, pageSize));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("count")]
    public async Task<IActionResult> GetContractsCount()
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetContractsCountQuery(userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("count-active")]
    public async Task<IActionResult> GetActiveContractsCount()
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetActiveContractsCountQuery(userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("count-completed")]
    public async Task<IActionResult> GetCompletedContractsCount()
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetCompletedContractsCountQuery(userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
