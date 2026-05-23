using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetRegionNameDtoList;

public record GetRegionNameDtoListQuery(string Lang):IRequest<Result<List<NameDto>>>;
