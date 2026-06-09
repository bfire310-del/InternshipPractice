using InternshipPractice.Domain.Dto;
using KDS.Primitives.FluentResult;

namespace InternshipPractice.Application.Interfaces.HttpClients;

public interface IAccessHttpClient
{
    Task<Result<LoginResponseDto>> Login(string email, string password);
}
