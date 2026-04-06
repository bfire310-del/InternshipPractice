using InternshipPractice.Application.Queries.GetAllStudents;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentController : BaseController
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await Mediator.Send(new GetAllStudentsQuery());

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
