using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetStatEmployerCabinet;

public class GetStatEmployerCabinetQueryHandler(IVacancyRepository vacancyRepository,
    IApplicationRepository applicationRepository) : IRequestHandler<GetStatEmployerCabinetQuery, Result<GetStatEmployerCabinetResponse>>
{
    public async Task<Result<GetStatEmployerCabinetResponse>> Handle(GetStatEmployerCabinetQuery request, CancellationToken cancellationToken)
    {
        GetStatEmployerCabinetResponse result = new GetStatEmployerCabinetResponse();
        var activeVacancies = vacancyRepository.GetActiveVacanciesByUserId(request.UserId);

        var responses = applicationRepository.GetResponsesCountByEmployerId(request.UserId);

        var congrats = applicationRepository.GetCongratsContracts(request.UserId);

        await Task.WhenAll(activeVacancies, responses, congrats);

        if(activeVacancies.Result.IsSuccess)
        {
            result.ActiveVacancies = activeVacancies.Result.Value;
        }

        if(responses.Result.IsSuccess)
        {
            result.Responses = responses.Result.Value;
        }

        if(congrats.Result.IsSuccess)
        {
            result.ConcludedContracts = congrats.Result.Value;
        }

        return result;
    }
}
