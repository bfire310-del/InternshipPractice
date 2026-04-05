namespace InternshipPractice.Domain.Entities;

public partial class University
{
    public Guid UniversityId { get; set; }

    public string NameRu { get; set; } = null!;

    public string? NameKk { get; set; }

    public string? NameEn { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
