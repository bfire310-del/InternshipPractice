using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetAllCompanies;

public record GetAllCompaniesQuery() : IRequest<Result<List<CompanyResponse>>>;
