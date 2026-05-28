using InternshipPractice.Application.Queries.GetCompanyCategorNameDtoList;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompanyCategoryController : BaseController
{
    [HttpGet("name-dtos")]
    public async Task<IActionResult> GetVacancyCategoryNameDtos(string lang)
    {
        var result = await Mediator.Send(new GetCompanyCategorNameDtoListQuery(lang));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
} 
