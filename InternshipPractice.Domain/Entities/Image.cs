using System;
using System.Collections.Generic;

namespace InternshipPractice.Infrastructure.Entities;

public partial class Image
{
    public Guid ImageId { get; set; }

    public byte[]? ImageData { get; set; }

    public string? MimeType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual ICollection<StudentCv> StudentCvs { get; set; } = new List<StudentCv>();
}
