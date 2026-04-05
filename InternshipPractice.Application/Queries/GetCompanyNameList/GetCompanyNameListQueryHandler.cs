using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompanyNameList;

public class GetCompanyNameListQueryHandler(ICompanyRepository companyRepository) : IRequestHandler<GetCompanyNameListQuery, Result<List<string?>>>
{
    public async Task<Result<List<string?>>> Handle(GetCompanyNameListQuery request, CancellationToken cancellationToken)
    {
        var result = await companyRepository.GetCompanyNameList(request.Lang);
        return result;
    }
}
