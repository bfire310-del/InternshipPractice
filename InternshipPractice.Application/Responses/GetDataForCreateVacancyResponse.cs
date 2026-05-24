using InternshipPractice.Domain.Dto;

namespace InternshipPractice.Application.Responses;

public class GetDataForCreateVacancyResponse
{
    public List<TypeOfEmploymentDto> TypeOfEmployments { get; set; }
    public List<PracticeFormDto> PracticeForms {  get; set; }   
    public List<WorkFormatDto> WorkFormats { get; set; }
    public List<VacancyCategoryDto> VacancyCategories { get; set; }
    public List<PaymentTypeDto> PaymentTypes { get; set; }
    public List<RegionDto> Regions {  get; set; }
}
