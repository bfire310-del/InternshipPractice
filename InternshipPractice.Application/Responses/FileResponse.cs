namespace InternshipPractice.Application.Responses;

public class FileResponse
{
    public byte[] FileBytes { get; init; } = [];
    public string ContentType { get; init; } = null!;
    public string FileName { get; init; } = null!;
}