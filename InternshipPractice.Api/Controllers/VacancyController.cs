using System.Security.Claims;
using InternshipPractice.Api.Requests;
using InternshipPractice.Application.Queries;
using InternshipPractice.Application.Queries.GetActiveVacanciesByCompanyId;
using InternshipPractice.Application.Queries.GetFilteredVacancyNameList;
using InternshipPractice.Application.Queries.GetVacanciesByEmployerId;
using InternshipPractice.Application.Queries.GetVacancyByLikeWord;
using InternshipPractice.Application.Queries.GetVacancyCount;
using InternshipPractice.Application.Queries.GetVacancyCountNew;
using InternshipPractice.Application.Queries.GetVacancyDetailsById;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public async Task<IActionResult> GetFilteredVacancies(VacancySearchRequest request)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();
        
        var result = await Mediator.Send(new GetFilteredVacancyNameListQuery(
            request.Query,
            request.RegionId,
            request.PaymentTypeId,
            request.DurationCode,
            request.CategoryId,
            request.CompanyId,
            request.Lang,
            request.Page,
            request.PageSize,
            userId));
        
        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("details")]
    [Authorize]
    public async Task<IActionResult> GetVacancyDetailsById(Guid id)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetVacancyDetailsByIdQuery(id, userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    [HttpGet("like-word")]
    public async Task<IActionResult> GetVacancyByLikeWord(string word)
    {
        var result = await Mediator.Send(new GetVacancyByLikeWordQuery(word));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveVacanciesByCompanyId(Guid companyId)
    {
        var result = await Mediator.Send(new GetActiveVacanciesByCompanyIdQuery(companyId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("my-vacancies")]
    public async Task<IActionResult> GetVacanciesByEmployerId(Guid employerId)
    {
        var result = await Mediator.Send(new GetVacanciesByEmployerIdQuery(employerId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [Authorize]
    [HttpGet("delete")]
    public async Task<IActionResult> DeleteVacancy(Guid vacancyId)
    {
        var result = await Mediator.Send(new DeleteVacancyQuery(vacancyId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok();
    }
}
