namespace InternshipPractice.Application.Responses;

public class DiaryEntryResponse
{
    public Guid DiaryEntryId { get; set; }

    public DateOnly WorkDate { get; set; }

    public string Attendance { get; set; } = null!;

    public string TaskName { get; set; } = null!;

    public string? Description { get; set; }
}