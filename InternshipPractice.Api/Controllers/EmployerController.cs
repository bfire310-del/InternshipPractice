using InternshipPractice.Application.Queries.GetEmployerCount;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployerController : BaseController
{
    
    [HttpGet("count")]
    public async Task<IActionResult> GetEmployerCount()
    {
        var result = await Mediator.Send(new GetEmployerCountQuery());

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
