namespace InternshipPractice.Domain.Entities;

public partial class CareerCenter
{
    public Guid CareerCentersId { get; set; }

    public Guid UserId { get; set; }

    public Guid UniversityId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual User User { get; set; } = null!;
}
