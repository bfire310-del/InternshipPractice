namespace InternshipPractice.Infrastructure.Options;

public class AccessOptions
{
    public static string SectionName = nameof(AccessOptions);
    public string BaseUrl { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
}
