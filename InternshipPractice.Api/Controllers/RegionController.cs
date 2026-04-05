using InternshipPractice.Application.Queries.GetRegionCount;
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
}
