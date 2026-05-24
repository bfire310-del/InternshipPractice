namespace InternshipPractice.Domain.Requests;

public class CreateVacancyRequest
{
    public Guid? EmployerId { get; set; }
    public string? NameRu { get; set; }
    public string? ShotrDescription { get; set; }
    public string? FullDescription { get; set; }
    public Guid TypeOfEmploymentsId { get; set; }
    public Guid PracticeFormId { get; set; }
    public Guid WorkFormatId { get; set; }
    public Guid? RegionId { get; set; }
    public Guid CategoryId { get; set; }
    public int Course { get; set; }
    public string? NeccessaryTasks { get; set; }
    public string? Requirements { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? Address { get; set; }
    public string? JobTitle { get; set; }
    public Guid PaymentTypeId { get; set; }
}
