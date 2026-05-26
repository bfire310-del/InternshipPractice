using InternshipPractice.Application.Responses;

namespace InternshipPractice.Application.Interfaces.Services;

public interface IFileGeneratorService
{
    FileResponse GenerateDocx(ContractDetailResponse contract);
}
