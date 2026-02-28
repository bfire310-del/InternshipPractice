using System;
using System.Collections.Generic;

namespace InternshipPractice.Infrastructure.Entities;

public partial class StudentSkillMap
{
    public Guid StudentSkillMapId { get; set; }

    public Guid StudentId { get; set; }

    public Guid SkillId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual Skill Skill { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
