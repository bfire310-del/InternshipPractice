using InternshipPractice.Application.Interfaces.Repositories;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetEmployerCount;

public class GetEmployerCountQueryHandler(IEmployerRepository employerRepository) : IRequestHandler<GetEmployerCountQuery, Result<int>>
{
    public async Task<Result<int>> Handle(GetEmployerCountQuery request, CancellationToken cancellationToken)
    {
        var result = await employerRepository.GetEmployerCount();
        return result;
    }
}
