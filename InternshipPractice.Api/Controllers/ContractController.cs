using System.Security.Claims;
using InternshipPractice.Api.Requests;
using InternshipPractice.Application.Commands.SignContract;
using InternshipPractice.Application.Queries.DownloadContract;
using InternshipPractice.Application.Queries.GetActiveContractsCount;
using InternshipPractice.Application.Queries.GetCompletedContractsCount;
using InternshipPractice.Application.Queries.GetContractDetails;
using InternshipPractice.Application.Queries.GetContractsByStuff;
using InternshipPractice.Application.Queries.GetContractsByUserId;
using InternshipPractice.Application.Queries.GetContractSignData;
using InternshipPractice.Application.Queries.GetWaitingForSignContractsCount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternshipPractice.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ContractController : BaseController
{
    
    [HttpGet("by-user")]
    public async Task<IActionResult> GetContractsByUserId([FromQuery] string lang = "ru", [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetContractsByUserIdQuery(userId, lang, page, pageSize));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    [HttpGet("by-stuff")]
    public async Task<IActionResult> GetContractsByStuff([FromQuery] string lang = "ru", [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetContractsByStuffQuery(userId, lang, page, pageSize));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    [HttpGet("details")]
    public async Task<IActionResult> GetContractDetails([FromQuery] Guid contractId, [FromQuery] string lang = "ru")
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetContractDetailsQuery(userId, lang, contractId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    [HttpGet("download")]
    public async Task<IActionResult> DownloadContract([FromQuery] Guid contractId, [FromQuery] string lang = "ru")
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(
            new DownloadContractQuery(userId, lang,  contractId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return File(
            result.Value.FileBytes,
            result.Value.ContentType,
            result.Value.FileName);
    }
    
    [HttpGet("sign-data")]
    public async Task<IActionResult> GetContractSignData([FromQuery] Guid contractId, [FromQuery] string lang = "ru")
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetContractSignDataQuery(userId, lang, contractId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
    
    [HttpPost("sign")]
    public async Task<IActionResult> SignContract([FromBody] SignContractRequest request)
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(
            new SignContractCommand(userId, request.ContractId, request.Signature, request.Lang));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("count-active")]
    public async Task<IActionResult> GetActiveContractsCount()
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetActiveContractsCountQuery(userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("count-waiting-sign")]
    public async Task<IActionResult> GetWaitingForSignContractsCount()
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetWaitingForSignContractsCountQuery(userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("count-completed")]
    public async Task<IActionResult> GetCompletedContractsCount()
    {
        var userIdValue = User.FindFirstValue("UserId");

        if (!Guid.TryParse(userIdValue, out var userId))
            return Unauthorized();

        var result = await Mediator.Send(new GetCompletedContractsCountQuery(userId));

        if (result.IsFailed)
            return ProblemResponse(result.Error);

        return Ok(result.Value);
    }
}
