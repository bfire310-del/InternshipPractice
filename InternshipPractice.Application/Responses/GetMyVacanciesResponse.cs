namespace InternshipPractice.Application.Responses;

public class GetMyVacanciesResponse
{
    public Guid VacancyId {  get; set; }
    public string VacancyNameRu {  get; set; }
    public string VacancyNameKk { get; set; }
    public string VacancyNameEn { get; set; }
    public string Description {  get; set; }
    public DateTime StartDate {  get; set; }
    public string VacancyStatusCode {  get; set; }
    public int Responses {  get; set; }
}
