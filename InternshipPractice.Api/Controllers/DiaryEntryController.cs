using System.Security.Claims;
using InternshipPractice.Api.Requests;
using InternshipPractice.Application.Commands.CreateOrUpdateDiaryEntry;
using InternshipPractice.Application.Commands.SignContract;
using InternshipPractice.Application.Queries.GetAllCompanies;
using InternshipPractice.Application.Queries.GetCompanyCount;
using InternshipPractice.Application.Queries.GetCompanyNameList;
using InternshipPractice.Application.Queries.GetDiaryEntries;
using InternshipPractice.Application.Queries.GetFilteredCompanyNameList;
using InternshipPractice.Application.Queries.GetFilteredVacancyNameList;
using InternshipPractice.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiaryEntryController : BaseController
{
    [HttpGet("current")]
    public async Task<IActionResult> GetDiaryEntries()
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();
        
        var result = await Mediator.Send(new GetDiaryEntriesQuery(userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateDiaryEntry([FromBody] CreateDiaryEntryRequest request)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(
            new CreateOrUpdateDiaryEntryCommand(userId, request));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result);
    }
} 
