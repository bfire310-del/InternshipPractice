namespace InternshipPractice.Domain.Entities;

public partial class User
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public Guid RoleId { get; set; }

    public Guid StatusId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual ICollection<CareerCenter> CareerCenters { get; set; } = new List<CareerCenter>();

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual ICollection<Employer> Employers { get; set; } = new List<Employer>();

    public virtual Role Role { get; set; } = null!;

    public virtual UserStatus Status { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<UserAction> UserActions { get; set; } = new List<UserAction>();
}
