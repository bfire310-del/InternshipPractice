namespace InternshipPractice.Domain.Entities;

public partial class Student
{
    public Guid StudentId { get; set; }

    public Guid UserId { get; set; }

    public Guid FacultyId { get; set; }

    public int Course { get; set; }

    public decimal? Gpa { get; set; }

    public Guid StatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual ICollection<EmployerAssesment> EmployerAssesments { get; set; } = new List<EmployerAssesment>();

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual StudentStatus Status { get; set; } = null!;

    public virtual ICollection<StudentAssesment> StudentAssesments { get; set; } = new List<StudentAssesment>();

    public virtual ICollection<StudentSkillMap> StudentSkillMaps { get; set; } = new List<StudentSkillMap>();

    public virtual User User { get; set; } = null!;
    public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
}
