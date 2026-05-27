namespace InternshipPractice.Domain.Entities;

public partial class ContractSignature
{
    public Guid ContractSignatureId { get; set; }

    public Guid ContractId { get; set; }

    public Guid SignerUserId { get; set; }

    public string SignerType { get; set; } = null!;

    public string Lang { get; set; } = null!;

    public string Signature { get; set; } = null!;

    public string SignedData { get; set; } = null!;

    public DateTime SignedAt { get; set; }

    public virtual Contract Contract { get; set; } = null!;
}
