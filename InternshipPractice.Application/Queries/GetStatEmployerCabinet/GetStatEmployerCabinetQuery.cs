using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetStatEmployerCabinet;

public record GetStatEmployerCabinetQuery(Guid UserId):IRequest<Result<GetStatEmployerCabinetResponse>>;
