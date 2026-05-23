using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetPaymentTypeNameDtoList;

public record GetPaymentTypeNameDtoListQuery(string Lang):IRequest<Result<List<NameDto>>>;
