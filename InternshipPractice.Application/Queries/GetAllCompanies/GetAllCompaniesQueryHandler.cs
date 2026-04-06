using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetAllCompanies;

public class GetAllCompaniesQueryHandler(ICompanyRepository companyRepository) : IRequestHandler<GetAllCompaniesQuery, Result<List<CompanyResponse>>>
{
    public async Task<Result<List<CompanyResponse>>> Handle(GetAllCompaniesQuery request, CancellationToken cancellationToken)
    {
        var result = await companyRepository.GetAll();
        return result;
    }
}
