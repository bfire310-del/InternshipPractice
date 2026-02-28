using System;
using System.Collections.Generic;

namespace InternshipPractice.Infrastructure.Entities;

public partial class Skill
{
    public Guid SkillId { get; set; }

    public string NameRu { get; set; } = null!;

    public string? NameKk { get; set; }

    public string? NameEn { get; set; }

    public virtual ICollection<StudentSkillMap> StudentSkillMaps { get; set; } = new List<StudentSkillMap>();
}
