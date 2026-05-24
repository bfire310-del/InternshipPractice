namespace InternshipPractice.Domain.Entities;

public partial class Application
{
    public Guid ApplicationId { get; set; }

    public Guid? StudentId { get; set; }
    public Guid? VacancyId { get; set; }
    public Guid? StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual Student Student { get; set; } = null!;
    public virtual Vacancy Vacancy { get; set; } = null!;
    public virtual ApplicationStatus ApplicationStatus { get; set; } = null!;
    public virtual Contract? Contract { get; set; }
}
