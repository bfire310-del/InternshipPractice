using System;
using System.Collections.Generic;

namespace InternshipPractice.Infrastructure.Entities;

public partial class UserAction
{
    public Guid UserActionId { get; set; }

    public Guid? ActionTypeId { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual ActionType? ActionType { get; set; }

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual User? User { get; set; }
}
