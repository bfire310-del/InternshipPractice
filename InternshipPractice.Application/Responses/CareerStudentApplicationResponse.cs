namespace InternshipPractice.Application.Responses;

public class CareerStudentApplicationResponse
{
    public Guid StudentId { get; set; }

    public string StudentFullName { get; set; } = null!;

    public string FacultyName { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string Period { get; set; } = null!;

    public string StatusName { get; set; } = null!;
}
