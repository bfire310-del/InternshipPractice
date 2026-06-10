namespace InternshipPractice.Domain.Requests;

public class CreateDiaryEntryRequest
{
    public DateOnly WorkDate { get; set; }

    public string Attendance { get; set; } = null!; // present / absent / remote

    public string TaskName { get; set; } = null!;

    public string? Description { get; set; }
}
