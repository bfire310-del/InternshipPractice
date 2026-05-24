using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using KDS.Primitives.FluentResult;
using MediatR;

namespace InternshipPractice.Application.Queries.GetDataForCreateVacancy;

public class GetDataForCreateVacancyQueryHandler(IWorkFormatRepository workFormatRepository,
    IPracticeFormRepository practiceFormRepository,
    ITypeOfEmploymentRepository typeOfEmploymentRepository,
    IVacancyCategoryRepository vacancyCategoryRepository,
    IPaymentTypeRepository paymentTypeRepository) : IRequestHandler<GetDataForCreateVacancyQuery, Result<GetDataForCreateVacancyResponse>>
{
    public async Task<Result<GetDataForCreateVacancyResponse>> Handle(GetDataForCreateVacancyQuery request, CancellationToken cancellationToken)
    {
        var workFormat = await workFormatRepository.GetAll();
        var practiceform = await practiceFormRepository.GetAll();
        var typeOfEmployment = await typeOfEmploymentRepository.GetAll();
        var vacancyCategories = await vacancyCategoryRepository.GetAll();
        var paymentTypes = await paymentTypeRepository.GetAll();

        GetDataForCreateVacancyResponse result = new GetDataForCreateVacancyResponse()
        {
            WorkFormats = workFormat.Value,
            PracticeForms = practiceform.Value,
            TypeOfEmployments = typeOfEmployment.Value,
            VacancyCategories = vacancyCategories.Value,
            PaymentTypes = paymentTypes.Value,
        };

        return result;
    }
}
