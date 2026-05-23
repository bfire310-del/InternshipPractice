using InternshipPractice.Application.Queries.GetRegionCount;
using InternshipPractice.Application.Queries.GetRegionNameDtoList;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionController : BaseController
{
    [HttpGet("count")]
    public async Task<IActionResult> GetRegionCount()
    {
        var result = await Mediator.Send(new GetRegionCountQuery());

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    [HttpGet("name-dtos")]
    public async Task<IActionResult> GetRegionNameDtos(string lang)
    {
        var result = await Mediator.Send(new GetRegionNameDtoListQuery(lang));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
