namespace InternshipPractice.Domain.Entities;

public partial class DiaryEntry
{
    public Guid DiaryEntryId { get; set; }
    public Guid ApplicationId { get; set; }
    public DateOnly WorkDate { get; set; }
    public string Attendance { get; set; } = null!;
    // present / absent / remote
    public string TaskName { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }
    public virtual Application Application { get; set; } = null!;
}
