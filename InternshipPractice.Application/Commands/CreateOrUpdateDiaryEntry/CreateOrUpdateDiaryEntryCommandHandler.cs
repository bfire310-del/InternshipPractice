using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.CreateOrUpdateDiaryEntry;

public class CreateOrUpdateDiaryEntryCommandHandler(IDiaryEntryRepository diaryEntryRepository) : IRequestHandler<CreateOrUpdateDiaryEntryCommand, Result>
{
    public async Task<Result> Handle(CreateOrUpdateDiaryEntryCommand request, CancellationToken cancellationToken)
    {
        var result = await diaryEntryRepository.CreateOrUpdateDiaryEntry(request.UserId, request.Request);
        return result;
    }
}
