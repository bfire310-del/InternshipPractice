using InternshipPractice.Api.Requests;
using InternshipPractice.Application.Commands.CreateVacancy;
using InternshipPractice.Application.Queries.GetDataForCreateVacancy;
using InternshipPractice.Application.Queries.GetEmployerCount;
using InternshipPractice.Application.Queries.GetMyVacancies;
using InternshipPractice.Application.Queries.GetStatEmployerCabinet;
using InternshipPractice.Domain.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployerController : BaseController
{
    
    [HttpGet("count")]
    public async Task<IActionResult> GetEmployerCount()
    {
        var result = await Mediator.Send(new GetEmployerCountQuery());
        
        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("stat-employer-cabinet")]
    public async Task<IActionResult> GetStatEmployerCabinet()
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;

        if (userIdClaim is null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);

        var result = await Mediator.Send(
            new GetStatEmployerCabinetQuery(userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("my-vacancies")]
    public async Task<IActionResult> GetMyVacancies()
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;

        if (userIdClaim is null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);

        var result = await Mediator.Send(new GetMyVacanciesQuery(userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("data-for-create-vacancy")]
    public async Task<IActionResult> GetDataForCreateVacancy()
    {
        var result = await Mediator.Send(new GetDataForCreateVacancyQuery());

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpPost("add-vacancy")]
    public async Task<IActionResult> CreateVacancy(CreateVacancyRequest request)
    {
        var userIdClaim = User.FindFirst("UserId")?.Value;

        if (userIdClaim is null)
            return Unauthorized();

        var userId = Guid.Parse(userIdClaim);
        request.EmployerId = userId;
        var result = await Mediator.Send(new CreateVacancyCommand(request));

        if(result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok();
    }
}
