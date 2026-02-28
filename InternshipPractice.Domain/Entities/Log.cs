using System;
using System.Collections.Generic;

namespace InternshipPractice.Infrastructure.Entities;

public partial class Log
{
    public Guid LogId { get; set; }

    public Guid? ActionId { get; set; }

    public string? LogLevel { get; set; }

    public string? Details { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual UserAction? Action { get; set; }
}
