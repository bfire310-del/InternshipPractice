namespace InternshipPractice.Domain.Entities;

public partial class Vacancy
{
    public Guid VacancyId { get; set; }

    public Guid? EmployerId { get; set; }

    public string NameRu { get; set; } = null!;

    public string? NameKk { get; set; }

    public string? NameEn { get; set; }

    public Guid? TypeOfEmploymentId { get; set; } // фулл тайм, парт тайм

    public Guid? PracticeFormId { get; set; }

    public Guid? WorkFormatId { get; set; }

    public Guid? RegionId { get; set; }

    public Guid? CategoryId { get; set; }

    public int? Course { get; set; }

    public Guid? PaymentTypeId { get; set; }

    public string? ShortDescription { get; set; }

    public string? FullDescription { get; set; }

    public string? NeccessaryTasks { get; set; }

    public string? Requirements { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Address { get; set; }

    public bool? IsNdaRequired { get; set; }

    public Guid? StatusId { get; set; }

    public string? JobTitle { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }
    public virtual Employer? Employer { get; set; }

    public virtual VacancyCategory? Category { get; set; }

    public virtual PracticeForm? PracticeForm { get; set; }
    public virtual PaymentType? PaymentType { get; set; }

    public virtual Region? Region { get; set; }

    public virtual VacancyStatus? Status { get; set; }

    public virtual ICollection<StudentAssesment> StudentAssesments { get; set; } = new List<StudentAssesment>();

    public virtual TypeOfEmployment? TypeOfEmployment { get; set; }

    public virtual ICollection<VacancyDocument> VacancyDocuments { get; set; } = new List<VacancyDocument>();

    public virtual WorkFormat? WorkFormat { get; set; }
}
