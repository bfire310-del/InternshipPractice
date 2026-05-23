using InternshipPractice.Application.Queries.GetPaymentTypeNameDtoList;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentTypeController : BaseController
{
    
    [HttpGet("name-dtos")]
    public async Task<IActionResult> GetPaymentTypeNameDtos(string lang)
    {
        var result = await Mediator.Send(new GetPaymentTypeNameDtoListQuery(lang));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
