namespace InternshipPractice.Domain.Entities;

public partial class StudentCv
{
    public Guid StudentCvId { get; set; }

    public Guid? UserId { get; set; }

    public Guid? RegionId { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public Guid? ImageId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual Image? Image { get; set; }
}
