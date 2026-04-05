using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompanyCount;

public class GetCompanyCountQueryHandler(ICompanyRepository companyRepository) : IRequestHandler<GetCompanyCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetCompanyCountQuery request, CancellationToken cancellationToken)
    {
        var result = await companyRepository.GetCompanyCount();
        return result;
    }
}
