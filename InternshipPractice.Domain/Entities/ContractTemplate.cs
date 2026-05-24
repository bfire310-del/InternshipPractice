namespace InternshipPractice.Domain.Entities;

public partial class ContractTemplate
{
    public Guid ContractTemplateId { get; set; }

    public string? NameRu { get; set; }
    public string? NameKk { get; set; }
    public string? NameEn { get; set; }

    public string ContentRu { get; set; } = null!;
    public string? ContentKk { get; set; }
    public string? ContentEn { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Guid? DeletedBy { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
