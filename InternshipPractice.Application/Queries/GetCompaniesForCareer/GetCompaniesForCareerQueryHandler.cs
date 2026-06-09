using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompaniesForCareer;

public class GetCompaniesForCareerQueryHandler(ICompanyRepository companyRepository) : IRequestHandler<GetCompaniesForCareerQuery, Result<List<CompanyForCareerResponse>>>
{
    public async Task<Result<List<CompanyForCareerResponse>>> Handle(GetCompaniesForCareerQuery request, CancellationToken cancellationToken)
    {
        var result = await companyRepository.GetCompaniesForCareer();
        return result;
    }
}
