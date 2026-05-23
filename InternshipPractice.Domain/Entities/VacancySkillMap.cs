namespace InternshipPractice.Domain.Entities;

public partial class VacancySkillMap
{
    public Guid VacancySkillMapId { get; set; }

    public Guid VacancyId { get; set; }

    public Guid SkillId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual Skill Skill { get; set; } = null!;

    public virtual Vacancy Vacancy { get; set; } = null!;
}
