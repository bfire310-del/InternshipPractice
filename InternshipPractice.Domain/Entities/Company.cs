using System;
using System.Collections.Generic;

namespace InternshipPractice.Infrastructure.Entities;

public partial class Company
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

    public virtual CompanyCategory? CompanyCategory { get; set; }

    public virtual ICollection<Employer> Employers { get; set; } = new List<Employer>();

    public virtual Region? Region { get; set; }

    public virtual User? User { get; set; }
}
