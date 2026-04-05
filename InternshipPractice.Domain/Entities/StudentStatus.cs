namespace InternshipPractice.Domain.Entities;

public partial class StudentStatus
{
    public Guid StudentStatusId { get; set; }

    public string NameRu { get; set; } = null!;

    public string? NameKk { get; set; }

    public string? NameEn { get; set; }

    public string Code { get; set; } = null!;

    public string? DescriptionKk { get; set; }

    public string? DescriptionRu { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
