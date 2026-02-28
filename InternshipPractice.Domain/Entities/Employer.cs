using System;
using System.Collections.Generic;

namespace InternshipPractice.Infrastructure.Entities;

public partial class Employer
{
    public Guid EmployerId { get; set; }

    public Guid? UserId { get; set; }

    public string? JobTitle { get; set; }

    public Guid? CompanyId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<EmployerAssesment> EmployerAssesments { get; set; } = new List<EmployerAssesment>();

    public virtual User? User { get; set; }
}
