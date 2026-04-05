namespace InternshipPractice.Domain.Entities;

public partial class CompanyCategory
{
    public Guid CompanyCategoryId { get; set; }

    public string? NameRu { get; set; }

    public string? NameKk { get; set; }

    public string? NameEn { get; set; }

    public string? DescriptionRu { get; set; }

    public string? DescriptionKk { get; set; }

    public string? DescriptionEn { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();
}
