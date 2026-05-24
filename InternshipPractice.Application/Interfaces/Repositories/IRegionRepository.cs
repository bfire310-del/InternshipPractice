using InternshipPractice.Application.Responses;
using InternshipPractice.Domain.Dto;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.Repositories;

public interface IRegionRepository
{
    Task<Result<int>> GetRegionCount();
    Task<Result<List<NameDto>>> GetRegionNameDtoList(string lang);
    Task<Result<List<RegionDto>>> GetAll();
}
