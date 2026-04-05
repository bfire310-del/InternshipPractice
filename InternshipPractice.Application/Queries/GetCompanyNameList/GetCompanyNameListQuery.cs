using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompanyNameList;

public record GetCompanyNameListQuery(string Lang):IRequest<Result<List<string?>>>;
