namespace InternshipPractice.Domain.Entities;

public partial class Contract
{
    public Guid ContractId { get; set; }

    public string ContractNumber { get; set; } = null!;

    public Guid ContractTemplateId { get; set; }

    public Guid ApplicationId { get; set; }

    public Guid StatusId { get; set; }

    public string GeneratedContentRu { get; set; } = null!;
    public string GeneratedContentKk { get; set; } = null!;
    public string GeneratedContentEn { get; set; } = null!;

    public DateTime? StudentSignedAt { get; set; }

    public DateTime? EmployerSignedAt { get; set; }

    public DateTime? UniversitySignedAt { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual ContractTemplate ContractTemplate { get; set; } = null!;

    public virtual Application Application { get; set; } = null!;

    public virtual ContractStatus Status { get; set; } = null!;
    public virtual ICollection<ContractSignature> ContractSignatures { get; set; } = new List<ContractSignature>();
}
