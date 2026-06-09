using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompaniesForCareer;

public record GetCompaniesForCareerQuery():IRequest<Result<List<CompanyForCareerResponse>>>;
