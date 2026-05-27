using System.Security.Claims;
using InternshipPractice.Api.Requests;
using InternshipPractice.Application.Commands.ApproveApplication;
using InternshipPractice.Application.Commands.CreateApplication;
using InternshipPractice.Application.Commands.RejectApplication;
using InternshipPractice.Application.Commands.WithdrawApplication;
using InternshipPractice.Application.Queries.GetApplicationsByStatus;
using InternshipPractice.Application.Queries.GetEmployerApplications;
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
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new CreateApplicationCommand(
            userId,
            request.VacancyId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result);
    }
    
    [HttpGet("by-status")]
    public async Task<IActionResult> GetApplicationsByStatus([FromQuery] string? statusCode, [FromQuery] string lang = "ru",
        [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetApplicationsByStatusQuery(userId, statusCode, lang, page,  pageSize));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("employer")]
    public async Task<IActionResult> GetEmployerApplications(
        [FromQuery] string? statusCode,
        [FromQuery] string lang = "ru",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetEmployerApplicationsQuery(userId, statusCode, lang, page, pageSize));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpPost("{applicationId:guid}/withdraw")]
    public async Task<IActionResult> WithdrawApplication(Guid applicationId)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(
            new WithdrawApplicationCommand(
                userId,
                applicationId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok();
    }
    
    [HttpPost("{applicationId:guid}/approve")]
    public async Task<IActionResult> ApproveApplication(Guid applicationId)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new ApproveApplicationCommand(userId, applicationId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok();
    }


    [HttpPost("{applicationId:guid}/reject")]
    public async Task<IActionResult> RejectApplication(Guid applicationId)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new RejectApplicationCommand(userId, applicationId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok();
    }
    
}
