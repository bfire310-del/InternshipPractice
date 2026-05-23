using InternshipPractice.Application.Queries.GetVacancyCategoryNameDtoList;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VacancyCategoryController : BaseController
{
    [HttpGet("name-dtos")]
    public async Task<IActionResult> GetVacancyCategoryNameDtos(string lang)
    {
        var result = await Mediator.Send(new GetVacancyCategoryNameDtoListQuery(lang));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
