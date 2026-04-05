namespace InternshipPractice.Domain.Entities;

public partial class Faculty
{
    public Guid FacultyId { get; set; }

    public string NameRu { get; set; } = null!;

    public string? NameKk { get; set; }

    public string? NameEn { get; set; }

    public string Code { get; set; } = null!;

    public Guid UniversityId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual University University { get; set; } = null!;
}
