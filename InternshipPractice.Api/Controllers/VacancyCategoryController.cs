using InternshipPractice.Application.Queries.GetVacancyCategoryNameList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacancyCategoryController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetVacancyCategoryNames(string lang)
    {
        var result = await Mediator.Send(new GetVacancyCategoryNameListQuery(lang));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
