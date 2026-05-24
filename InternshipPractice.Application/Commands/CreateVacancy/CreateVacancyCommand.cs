
using InternshipPractice.Domain.Requests;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Commands.CreateVacancy;

public record CreateVacancyCommand(CreateVacancyRequest Request) : IRequest<Result>;
