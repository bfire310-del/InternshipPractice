using InternshipPractice.Domain.Requests;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.CreateOrUpdateDiaryEntry;

public record CreateOrUpdateDiaryEntryCommand(
    Guid UserId,
    CreateDiaryEntryRequest Request
    ):IRequest<Result>;
