using InternshipPractice.Api.Requests;
using InternshipPractice.Application.Queries.Login;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccessController : BaseController
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await Mediator.Send(new LoginQuery(request.Email, request.Password));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
