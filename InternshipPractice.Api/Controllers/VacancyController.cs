using InternshipPractice.Api.Requests;
using InternshipPractice.Application.Queries.GetFilteredVacancyNameList;
using InternshipPractice.Application.Queries.GetVacancyCount;
using InternshipPractice.Application.Queries.GetVacancyCountNew;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacancyController : BaseController
{
    [HttpGet("count")]
    public async Task<IActionResult> GetVacancyCount()
    {
        var result = await Mediator.Send(new GetVacancyCountQuery());

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    [HttpGet("count-new")]
    public async Task<IActionResult> GetVacancyCountNew()
    {
        var result = await Mediator.Send(new GetVacancyCountNewQuery());

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    
    [HttpPost("filtered")]
    public async Task<IActionResult> GetFilteredVacancies(VacancySearchRequest request)
    {
        var result = await Mediator.Send(new GetFilteredVacancyNameListQuery(
            request.Query,
            request.RegionId,
            request.CategoryId,
            request.WorkFormatId,
            request.PracticeFormId,
            request.TypeOfEmploymentId,
            request.Course,
            request.OnlyPublished ?? true,
            request.OnlyPaid,
            request.DurationMonthsMin,
            request.DurationMonthsMax,
            request.Lang,
            request.Page,
            request.PageSize));
        
        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
