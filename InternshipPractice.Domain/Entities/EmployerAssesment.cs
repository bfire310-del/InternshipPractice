using System;
using System.Collections.Generic;

namespace InternshipPractice.Infrastructure.Entities;

public partial class EmployerAssesment
{
    public Guid EmployerAssesmentId { get; set; }

    public Guid? EmployerId { get; set; }

    public Guid? StudentId { get; set; }

    public decimal? FinalScore { get; set; }

    public string? FinalReview { get; set; }

    public Guid? StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual Employer? Employer { get; set; }

    public virtual EmployerAssesmentStatus? Status { get; set; }

    public virtual Student? Student { get; set; }
}
