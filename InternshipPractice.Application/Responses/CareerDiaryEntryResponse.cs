namespace InternshipPractice.Application.Responses;

public class CareerDiaryEntryResponse
{
    public Guid DiaryId { get; set; }

    public DateOnly Date { get; set; }

    public string Task { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string PresenceStatusName { get; set; } = null!;
}
