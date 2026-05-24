using InternshipPractice.Domain.Dto;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IWorkFormatRepository
{
    Task<Result<List<WorkFormatDto>>> GetAll();
}
