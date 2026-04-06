namespace InternshipPractice.Application.Responses;

public class StudentResponse
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public int Course { get; set; }

    public decimal? Gpa { get; set; }

    public string StatusName {  get; set; }
    public List<string> SkillsMap {  get; set; }

}
