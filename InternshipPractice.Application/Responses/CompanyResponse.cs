namespace InternshipPractice.Application.Responses;

public class CompanyResponse
{
    public Guid CompanyId { get; set; }

    public Guid? UserId { get; set; }

    public string? CompanyNameRu { get; set; }

    public string? CompanyDescriptionRu { get; set; }

    public string? CompanyNameKk { get; set; }

    public string? CompanyDescriptionKk { get; set; }

    public string? CompanyNameEn { get; set; }

    public string? CompanyDescriptionEn { get; set; }

    public Guid? CompanyCategoryId { get; set; }

    public string? LinkToWebsite { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public Guid? RegionId { get; set; }
}
