using System.Security.Claims;
using InternshipPractice.Api.Requests;
using InternshipPractice.Application.Queries.GetAllCompanies;
using InternshipPractice.Application.Queries.GetCompanyCount;
using InternshipPractice.Application.Queries.GetCompanyNameList;
using InternshipPractice.Application.Queries.GetFilteredCompanyNameList;
using InternshipPractice.Application.Queries.GetFilteredVacancyNameList;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyController : BaseController
{
    [HttpGet("names")]
    public async Task<IActionResult> GetCompanyNames(string lang)
    {
        var result = await Mediator.Send(new GetCompanyNameListQuery(lang));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    [HttpGet("count")]
    public async Task<IActionResult> GetCompanyCount()
    {
        var result = await Mediator.Send(new GetCompanyCountQuery());

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCompanies()
    {
        var result = await Mediator.Send(new GetAllCompaniesQuery());

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    [HttpPost("filtered")]
    public async Task<IActionResult> GetFilteredCompanies(CompanySearchRequest request)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();
        
        var result = await Mediator.Send(new GetFilteredCompanyNameListQuery(
            request.Query,
            request.RegionId,
            request.CategoryId,
            request.Lang,
            request.Page,
            request.PageSize,
            userId));
        
        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
} 
