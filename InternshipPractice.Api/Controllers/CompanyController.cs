using InternshipPractice.Application.Queries.GetAllCompanies;
using InternshipPractice.Application.Queries.GetCompanyCount;
using InternshipPractice.Application.Queries.GetCompanyNameList;
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
} 
