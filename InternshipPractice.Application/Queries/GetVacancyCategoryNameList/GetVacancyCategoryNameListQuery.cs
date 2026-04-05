using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyCategoryNameList;

public record GetVacancyCategoryNameListQuery(string Lang):IRequest<Result<List<string?>>>;
