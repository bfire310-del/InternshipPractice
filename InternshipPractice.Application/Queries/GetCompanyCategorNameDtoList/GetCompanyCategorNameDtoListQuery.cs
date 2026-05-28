using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetCompanyCategorNameDtoList;

public record GetCompanyCategorNameDtoListQuery(string Lang):IRequest<Result<List<NameDto>>>;
