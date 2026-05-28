using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetFilteredCompanyNameList;

public class GetFilteredCompanyNameListQueryHandler(ICompanyRepository companyRepository) : IRequestHandler<GetFilteredCompanyNameListQuery, Result<PagedResult<CompanySearchResponse>>>
{
    public async Task<Result<PagedResult<CompanySearchResponse>>> Handle(GetFilteredCompanyNameListQuery request, CancellationToken cancellationToken)
    {
        var result = await companyRepository.GetFilteredCompanyNamesAsync(request.Query,
            request.RegionId,
            request.CategoryId,
            request.Lang,
            request.Page,
            request.PageSize,
            request.UserId,
            cancellationToken);
        return result;
    }
}
