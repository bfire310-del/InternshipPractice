using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyDetailsById;

public class GetVacancyDetailsByIdQueryHandler(IVacancyRepository vacancyRepository) : IRequestHandler<GetVacancyDetailsByIdQuery, Result<VacancyDetailResponse>>
{
    public async Task<Result<VacancyDetailResponse>> Handle(GetVacancyDetailsByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await vacancyRepository.GetVacancyDetailsById(request.Id, request.UserId);
        return result;
    }
}
