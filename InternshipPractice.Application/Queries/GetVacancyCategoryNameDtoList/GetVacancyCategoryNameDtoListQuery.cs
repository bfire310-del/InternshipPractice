using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetVacancyCategoryNameDtoList;

public record GetVacancyCategoryNameDtoListQuery(string Lang):IRequest<Result<List<NameDto>>>;
