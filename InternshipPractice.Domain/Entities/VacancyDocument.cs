using System;
using System.Collections.Generic;

namespace InternshipPractice.Infrastructure.Entities;

public partial class VacancyDocument
{
    public Guid VacancyDocumentId { get; set; }

    public Guid? VacancyId { get; set; }

    public string? Name { get; set; }

    public string? Content { get; set; }

    public long? Size { get; set; }

    public string? Extension { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual Vacancy? Vacancy { get; set; }
}
