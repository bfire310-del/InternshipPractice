using InternshipPractice.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Data;

public partial class InternshipPracticeDbContext : DbContext
{
    public InternshipPracticeDbContext(DbContextOptions<InternshipPracticeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActionType> ActionTypes { get; set; }

    public virtual DbSet<CareerCenter> CareerCenters { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CompanyCategory> CompanyCategories { get; set; }

    public virtual DbSet<Employer> Employers { get; set; }

    public virtual DbSet<EmployerAssesment> EmployerAssesments { get; set; }

    public virtual DbSet<EmployerAssesmentStatus> EmployerAssesmentStatuses { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<PracticeForm> PracticeForms { get; set; }
    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentAssesment> StudentAssesments { get; set; }

    public virtual DbSet<StudentAssesmentStatus> StudentAssesmentStatuses { get; set; }

    public virtual DbSet<StudentCv> StudentCvs { get; set; }

    public virtual DbSet<StudentSkillMap> StudentSkillMaps { get; set; }

    public virtual DbSet<StudentStatus> StudentStatuses { get; set; }

    public virtual DbSet<TypeOfEmployment> TypeOfEmployments { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAction> UserActions { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    public virtual DbSet<Vacancy> Vacancies { get; set; }

    public virtual DbSet<VacancyCategory> VacancyCategories { get; set; }

    public virtual DbSet<VacancyDocument> VacancyDocuments { get; set; }

    public virtual DbSet<VacancyStatus> VacancyStatuses { get; set; }
    public virtual DbSet<VacancySkillMap> VacancySkillMaps { get; set; }

    public virtual DbSet<WorkFormat> WorkFormats { get; set; }
    
    public virtual DbSet<Domain.Entities.Application> Applications { get; set; }
    public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
    
    public virtual DbSet<Contract> Contracts { get; set; }
    public virtual DbSet<ContractTemplate> ContractTemplates { get; set; }
    public virtual DbSet<ContractStatus> ContractStatuses { get; set; }
    public virtual DbSet<ContractSignature> ContractSignatures { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActionType>(entity =>
        {
            entity.HasKey(e => e.ActionTypeId).HasName("action_types_pkey");

            entity.ToTable("action_types");

            entity.Property(e => e.ActionTypeId)
                .ValueGeneratedNever()
                .HasColumnName("action_type_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<CareerCenter>(entity =>
        {
            entity.HasKey(e => e.CareerCentersId).HasName("career_centers_pkey");

            entity.ToTable("career_centers");

            entity.Property(e => e.CareerCentersId)
                .ValueGeneratedNever()
                .HasColumnName("career_centers_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.CareerCenters)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_career_centers_user");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("companies_pkey");

            entity.ToTable("companies");

            entity.Property(e => e.CompanyId)
                .ValueGeneratedNever()
                .HasColumnName("company_id");
            entity.Property(e => e.CompanyCategoryId).HasColumnName("company_category_id");
            entity.Property(e => e.CompanyDescriptionEn).HasColumnName("company_description_en");
            entity.Property(e => e.CompanyDescriptionKk).HasColumnName("company_description_kk");
            entity.Property(e => e.CompanyDescriptionRu).HasColumnName("company_description_ru");
            entity.Property(e => e.CompanyNameEn)
                .HasMaxLength(150)
                .HasColumnName("company_name_en");
            entity.Property(e => e.CompanyNameKk)
                .HasMaxLength(150)
                .HasColumnName("company_name_kk");
            entity.Property(e => e.CompanyNameRu)
                .HasMaxLength(150)
                .HasColumnName("company_name_ru");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.LinkToWebsite).HasColumnName("link_to_website");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.CompanyCategory).WithMany(p => p.Companies)
                .HasForeignKey(d => d.CompanyCategoryId)
                .HasConstraintName("fk_companies_category");

            entity.HasOne(d => d.Region).WithMany(p => p.Companies)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("fk_companies_region");

            entity.HasOne(d => d.User).WithMany(p => p.Companies)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_companies_user");
        });

        modelBuilder.Entity<CompanyCategory>(entity =>
        {
            entity.HasKey(e => e.CompanyCategoryId).HasName("company_categories_pkey");

            entity.ToTable("company_categories");

            entity.Property(e => e.CompanyCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("company_category_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Employer>(entity =>
        {
            entity.HasKey(e => e.EmployerId).HasName("employers_pkey");

            entity.ToTable("employers");

            entity.Property(e => e.EmployerId)
                .ValueGeneratedNever()
                .HasColumnName("employer_id");
            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.JobTitle).HasColumnName("job_title");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Company).WithMany(p => p.Employers)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("fk_employers_company");

            entity.HasOne(d => d.User).WithMany(p => p.Employers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_employers_user");
        });

        modelBuilder.Entity<EmployerAssesment>(entity =>
        {
            entity.HasKey(e => e.EmployerAssesmentId).HasName("employer_assesments_pkey");

            entity.ToTable("employer_assesments");

            entity.Property(e => e.EmployerAssesmentId)
                .ValueGeneratedNever()
                .HasColumnName("employer_assesment_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.EmployerId).HasColumnName("employer_id");
            entity.Property(e => e.FinalReview).HasColumnName("final_review");
            entity.Property(e => e.FinalScore)
                .HasPrecision(3, 2)
                .HasColumnName("final_score");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Employer).WithMany(p => p.EmployerAssesments)
                .HasForeignKey(d => d.EmployerId)
                .HasConstraintName("fk_employer_assesments_employer");

            entity.HasOne(d => d.Status).WithMany(p => p.EmployerAssesments)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_employer_assesments_status");

            entity.HasOne(d => d.Student).WithMany(p => p.EmployerAssesments)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("fk_employer_assesments_student");
        });

        modelBuilder.Entity<EmployerAssesmentStatus>(entity =>
        {
            entity.HasKey(e => e.EmployerAssesmentStatusId).HasName("employer_assesment_statuses_pkey");

            entity.ToTable("employer_assesment_statuses");

            entity.Property(e => e.EmployerAssesmentStatusId)
                .ValueGeneratedNever()
                .HasColumnName("employer_assesment_status_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("faculties_pkey");

            entity.ToTable("faculties");

            entity.Property(e => e.FacultyId)
                .ValueGeneratedNever()
                .HasColumnName("faculty_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UniversityId).HasColumnName("university_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.University).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.UniversityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_faculties_university");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("images_pkey");

            entity.ToTable("images");

            entity.Property(e => e.ImageId)
                .ValueGeneratedNever()
                .HasColumnName("image_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.ImageData).HasColumnName("image_data");
            entity.Property(e => e.MimeType)
                .HasMaxLength(100)
                .HasColumnName("mime_type");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("logs_pkey");

            entity.ToTable("logs");

            entity.Property(e => e.LogId)
                .ValueGeneratedNever()
                .HasColumnName("log_id");
            entity.Property(e => e.ActionId).HasColumnName("action_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Details).HasColumnName("details");
            entity.Property(e => e.LogLevel)
                .HasMaxLength(50)
                .HasColumnName("log_level");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Action).WithMany(p => p.Logs)
                .HasForeignKey(d => d.ActionId)
                .HasConstraintName("fk_logs_action");
        });

        modelBuilder.Entity<PracticeForm>(entity =>
        {
            entity.HasKey(e => e.PracticeFormId).HasName("practice_forms_pkey");

            entity.ToTable("practice_forms");

            entity.Property(e => e.PracticeFormId)
                .ValueGeneratedNever()
                .HasColumnName("practice_form_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("regions_pkey");

            entity.ToTable("regions");

            entity.Property(e => e.RegionId)
                .ValueGeneratedNever()
                .HasColumnName("region_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("role_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.SkillId).HasName("skills_pkey");

            entity.ToTable("skills");

            entity.Property(e => e.SkillId)
                .ValueGeneratedNever()
                .HasColumnName("skill_id");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("students_pkey");

            entity.ToTable("students");

            entity.Property(e => e.StudentId)
                .ValueGeneratedNever()
                .HasColumnName("student_id");
            entity.Property(e => e.Course).HasColumnName("course");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.FacultyId).HasColumnName("faculty_id");
            entity.Property(e => e.Gpa)
                .HasPrecision(3, 2)
                .HasColumnName("gpa");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Students)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_students_faculty");

            entity.HasOne(d => d.Status).WithMany(p => p.Students)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_students_status");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_students_user");
        });

        modelBuilder.Entity<StudentAssesment>(entity =>
        {
            entity.HasKey(e => e.StudentAssesmentId).HasName("student_assesments_pkey");

            entity.ToTable("student_assesments");

            entity.Property(e => e.StudentAssesmentId)
                .ValueGeneratedNever()
                .HasColumnName("student_assesment_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.FinalReview).HasColumnName("final_review");
            entity.Property(e => e.InitiativeAndIndependence).HasColumnName("initiative_and_independence");
            entity.Property(e => e.InitiativeAndIndependenceComment).HasColumnName("initiative_and_independence_comment");
            entity.Property(e => e.OverallFinalScore).HasColumnName("overall_final_score");
            entity.Property(e => e.OverallFinalScoreComment).HasColumnName("overall_final_score_comment");
            entity.Property(e => e.ProfessionalKnowledge).HasColumnName("professional_knowledge");
            entity.Property(e => e.ProfessionalKnowledgeComment).HasColumnName("professional_knowledge_comment");
            entity.Property(e => e.PunctuallyAndDiscipline).HasColumnName("punctually_and_discipline");
            entity.Property(e => e.PunctuallyAndDisciplineComment).HasColumnName("punctually_and_discipline_comment");
            entity.Property(e => e.QualityOfTheAssignedTasks).HasColumnName("quality_of_the_assigned_tasks");
            entity.Property(e => e.QualityOfTheAssignedTasksComment).HasColumnName("quality_of_the_assigned_tasks_comment");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TeamworkAndCommunication).HasColumnName("teamwork_and_communication");
            entity.Property(e => e.TeamworkAndCommunicationComment).HasColumnName("teamwork_and_communication_comment");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");

            entity.HasOne(d => d.Status).WithMany(p => p.StudentAssesments)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_student_assesments_status");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentAssesments)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("fk_student_assesments_student");

            entity.HasOne(d => d.Vacancy).WithMany(p => p.StudentAssesments)
                .HasForeignKey(d => d.VacancyId)
                .HasConstraintName("fk_student_assesments_vacancy");
        });

        modelBuilder.Entity<StudentAssesmentStatus>(entity =>
        {
            entity.HasKey(e => e.StudentAssesmentStatusId).HasName("student_assesment_statuses_pkey");

            entity.ToTable("student_assesment_statuses");

            entity.Property(e => e.StudentAssesmentStatusId)
                .ValueGeneratedNever()
                .HasColumnName("student_assesment_status_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<StudentCv>(entity =>
        {
            entity.HasKey(e => e.StudentCvId).HasName("student_cvs_pkey");

            entity.ToTable("student_cvs");

            entity.Property(e => e.StudentCvId)
                .ValueGeneratedNever()
                .HasColumnName("student_cv_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Image).WithMany(p => p.StudentCvs)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("fk_student_cvs_image");
        });

        modelBuilder.Entity<StudentSkillMap>(entity =>
        {
            entity.HasKey(e => e.StudentSkillMapId).HasName("student_skill_map_pkey");

            entity.ToTable("student_skill_map");

            entity.Property(e => e.StudentSkillMapId)
                .ValueGeneratedNever()
                .HasColumnName("student_skill_map_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.SkillId).HasColumnName("skill_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Skill).WithMany(p => p.StudentSkillMaps)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_skill_map_skill");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentSkillMaps)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_student_skill_map_student");
        });

        modelBuilder.Entity<StudentStatus>(entity =>
        {
            entity.HasKey(e => e.StudentStatusId).HasName("student_statuses_pkey");

            entity.ToTable("student_statuses");

            entity.Property(e => e.StudentStatusId)
                .ValueGeneratedNever()
                .HasColumnName("student_status_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<TypeOfEmployment>(entity =>
        {
            entity.HasKey(e => e.TypeOfEmploymentId).HasName("type_of_employments_pkey");

            entity.ToTable("type_of_employments");

            entity.Property(e => e.TypeOfEmploymentId)
                .ValueGeneratedNever()
                .HasColumnName("type_of_employment_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<University>(entity =>
        {
            entity.HasKey(e => e.UniversityId).HasName("universities_pkey");

            entity.ToTable("universities");

            entity.Property(e => e.UniversityId)
                .ValueGeneratedNever()
                .HasColumnName("university_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(100)
                .HasColumnName("patronymic");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_role");

            entity.HasOne(d => d.Status).WithMany(p => p.Users)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_users_status");
        });

        modelBuilder.Entity<UserAction>(entity =>
        {
            entity.HasKey(e => e.UserActionId).HasName("user_actions_pkey");

            entity.ToTable("user_actions");

            entity.Property(e => e.UserActionId)
                .ValueGeneratedNever()
                .HasColumnName("user_action_id");
            entity.Property(e => e.ActionTypeId).HasColumnName("action_type_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.ActionType).WithMany(p => p.UserActions)
                .HasForeignKey(d => d.ActionTypeId)
                .HasConstraintName("fk_user_actions_type");

            entity.HasOne(d => d.User).WithMany(p => p.UserActions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("fk_user_actions_user");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.UserStatusId).HasName("user_statuses_pkey");

            entity.ToTable("user_statuses");

            entity.Property(e => e.UserStatusId)
                .ValueGeneratedNever()
                .HasColumnName("user_status_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<Vacancy>(entity =>
        {
            entity.HasKey(e => e.VacancyId).HasName("vacancies_pkey");

            entity.ToTable("vacancies");

            entity.Property(e => e.VacancyId)
                .ValueGeneratedNever()
                .HasColumnName("vacancy_id");
            entity.Property(e => e.Address)
                .HasMaxLength(300)
                .HasColumnName("address");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Course).HasColumnName("course");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.EmployerId).HasColumnName("employer_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.FullDescription).HasColumnName("full_description");
            entity.Property(e => e.IsNdaRequired).HasColumnName("is_nda_required");
            entity.Property(e => e.JobTitle).HasColumnName("job_title");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.NeccessaryTasks).HasColumnName("neccessary_tasks");
            entity.Property(e => e.PaymentTypeId).HasColumnName("payment_type_id");
            entity.Property(e => e.PracticeFormId).HasColumnName("practice_form_id");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.Requirements).HasColumnName("requirements");
            entity.Property(e => e.ShortDescription)
                .HasMaxLength(150)
                .HasColumnName("short_description");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TypeOfEmploymentId).HasColumnName("type_of_employment_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.WorkFormatId).HasColumnName("work_format_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("fk_vacancies_category");

            entity.HasOne(d => d.PracticeForm).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.PracticeFormId)
                .HasConstraintName("fk_vacancies_practice_form");
            
            entity.HasOne(d => d.PaymentType).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.PaymentTypeId)
                .HasConstraintName("fk_vacancies_payment_type");

            entity.HasOne(d => d.Region).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("fk_vacancies_region");

            entity.HasOne(d => d.Status).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_vacancies_status");

            entity.HasOne(d => d.TypeOfEmployment).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.TypeOfEmploymentId)
                .HasConstraintName("fk_vacancies_type_of_employment");

            entity.HasOne(d => d.WorkFormat).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.WorkFormatId)
                .HasConstraintName("fk_vacancies_work_format");
        });

        modelBuilder.Entity<VacancyCategory>(entity =>
        {
            entity.HasKey(e => e.VacancyCategoryId).HasName("vacancy_categories_pkey");

            entity.ToTable("vacancy_categories");

            entity.Property(e => e.VacancyCategoryId)
                .ValueGeneratedNever()
                .HasColumnName("vacancy_category_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.PaymentTypeId).HasName("payment_types_pkey");

            entity.ToTable("payment_types");

            entity.Property(e => e.PaymentTypeId)
                .ValueGeneratedNever()
                .HasColumnName("payment_type_id");

            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");

            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");

            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");

            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");

            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
        });
        
        modelBuilder.Entity<VacancyDocument>(entity =>
        {
            entity.HasKey(e => e.VacancyDocumentId).HasName("vacancy_documents_pkey");

            entity.ToTable("vacancy_documents");

            entity.Property(e => e.VacancyDocumentId)
                .ValueGeneratedNever()
                .HasColumnName("vacancy_document_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.Extension)
                .HasMaxLength(20)
                .HasColumnName("extension");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");

            entity.HasOne(d => d.Vacancy).WithMany(p => p.VacancyDocuments)
                .HasForeignKey(d => d.VacancyId)
                .HasConstraintName("fk_vacancy_documents_vacancy");
        });

        modelBuilder.Entity<VacancyStatus>(entity =>
        {
            entity.HasKey(e => e.VacancyStatusId).HasName("vacancy_statuses_pkey");

            entity.ToTable("vacancy_statuses");

            entity.Property(e => e.VacancyStatusId)
                .ValueGeneratedNever()
                .HasColumnName("vacancy_status_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });
        
        modelBuilder.Entity<VacancySkillMap>(entity =>
        {
            entity.HasKey(e => e.VacancySkillMapId).HasName("vacancy_skill_map_pkey");

            entity.ToTable("vacancy_skill_map");

            entity.HasIndex(e => new { e.VacancyId, e.SkillId })
                .IsUnique()
                .HasDatabaseName("ux_vacancy_skill_map_vacancy_skill")
                .HasFilter("deleted_at IS NULL");

            entity.Property(e => e.VacancySkillMapId)
                .ValueGeneratedNever()
                .HasColumnName("vacancy_skill_map_id");

            entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");
            entity.Property(e => e.SkillId).HasColumnName("skill_id");

            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

            entity.HasOne(d => d.Vacancy)
                .WithMany(p => p.VacancySkillMaps)
                .HasForeignKey(d => d.VacancyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vacancy_skill_map_vacancy");

            entity.HasOne(d => d.Skill)
                .WithMany(p => p.VacancySkillMaps)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_vacancy_skill_map_skill");
        });

        modelBuilder.Entity<WorkFormat>(entity =>
        {
            entity.HasKey(e => e.WorkFormatId).HasName("work_formats_pkey");

            entity.ToTable("work_formats");

            entity.Property(e => e.WorkFormatId)
                .ValueGeneratedNever()
                .HasColumnName("work_format_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");
            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");
            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });
        
        modelBuilder.Entity<ApplicationStatus>(entity =>
        {
            entity.HasKey(e => e.ApplicationStatusId)
                .HasName("application_statuses_pkey");

            entity.ToTable("application_statuses");

            entity.HasIndex(e => e.Code)
                .IsUnique()
                .HasDatabaseName("ux_application_statuses_code")
                .HasFilter("deleted_at IS NULL");

            entity.Property(e => e.ApplicationStatusId)
                .ValueGeneratedNever()
                .HasColumnName("application_status_id");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");

            entity.Property(e => e.NameRu)
                .HasMaxLength(150)
                .HasColumnName("name_ru");

            entity.Property(e => e.NameKk)
                .HasMaxLength(150)
                .HasColumnName("name_kk");

            entity.Property(e => e.NameEn)
                .HasMaxLength(150)
                .HasColumnName("name_en");

            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");

            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
        });
        
        modelBuilder.Entity<Domain.Entities.Application>(entity =>
        {
            entity.HasKey(e => e.ApplicationId)
                .HasName("applications_pkey");

            entity.ToTable("applications");

            entity.HasIndex(e => new { e.StudentId, e.VacancyId })
                .IsUnique()
                .HasDatabaseName("ux_applications_student_vacancy")
                .HasFilter("deleted_at IS NULL");

            entity.Property(e => e.ApplicationId)
                .ValueGeneratedNever()
                .HasColumnName("application_id");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.VacancyId).HasColumnName("vacancy_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

            entity.HasOne(d => d.Student)
                .WithMany(p => p.Applications)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("fk_applications_student");

            entity.HasOne(d => d.Vacancy)
                .WithMany(p => p.Applications)
                .HasForeignKey(d => d.VacancyId)
                .HasConstraintName("fk_applications_vacancy");

            entity.HasOne(d => d.ApplicationStatus)
                .WithMany(p => p.Applications)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("fk_applications_status");
        });
        
        modelBuilder.Entity<ContractTemplate>(entity =>
        {
            entity.HasKey(e => e.ContractTemplateId).HasName("contract_templates_pkey");

            entity.ToTable("contract_templates");

            entity.Property(e => e.ContractTemplateId)
                .ValueGeneratedNever()
                .HasColumnName("contract_template_id");

            entity.Property(e => e.NameRu).HasMaxLength(150).HasColumnName("name_ru");
            entity.Property(e => e.NameKk).HasMaxLength(150).HasColumnName("name_kk");
            entity.Property(e => e.NameEn).HasMaxLength(150).HasColumnName("name_en");

            entity.Property(e => e.ContentRu).HasColumnName("content_ru");
            entity.Property(e => e.ContentKk).HasColumnName("content_kk");
            entity.Property(e => e.ContentEn).HasColumnName("content_en");

            entity.Property(e => e.IsActive).HasColumnName("is_active");

            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
        });
        
        modelBuilder.Entity<ContractStatus>(entity =>
        {
            entity.HasKey(e => e.ContractStatusId).HasName("contract_statuses_pkey");

            entity.ToTable("contract_statuses");

            entity.HasIndex(e => e.Code)
                .IsUnique()
                .HasDatabaseName("contract_statuses_code_key");

            entity.Property(e => e.ContractStatusId)
                .ValueGeneratedNever()
                .HasColumnName("contract_status_id");

            entity.Property(e => e.Code).HasMaxLength(50).HasColumnName("code");

            entity.Property(e => e.NameRu).HasMaxLength(150).HasColumnName("name_ru");
            entity.Property(e => e.NameKk).HasMaxLength(150).HasColumnName("name_kk");
            entity.Property(e => e.NameEn).HasMaxLength(150).HasColumnName("name_en");

            entity.Property(e => e.DescriptionRu).HasColumnName("description_ru");
            entity.Property(e => e.DescriptionKk).HasColumnName("description_kk");
            entity.Property(e => e.DescriptionEn).HasColumnName("description_en");

            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");
        });
        
        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.ContractId).HasName("contracts_pkey");

            entity.ToTable("contracts");

            entity.HasIndex(e => e.ContractNumber)
                .IsUnique()
                .HasDatabaseName("ux_contracts_contract_number")
                .HasFilter("deleted_at IS NULL");

            entity.HasIndex(e => e.ApplicationId)
                .IsUnique()
                .HasDatabaseName("ux_contracts_application")
                .HasFilter("deleted_at IS NULL");

            entity.Property(e => e.ContractId)
                .ValueGeneratedNever()
                .HasColumnName("contract_id");

            entity.Property(e => e.ContractNumber)
                .HasMaxLength(100)
                .HasColumnName("contract_number");

            entity.Property(e => e.ContractTemplateId).HasColumnName("contract_template_id");
            entity.Property(e => e.ApplicationId).HasColumnName("application_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.Property(e => e.GeneratedContentRu).HasColumnName("generated_content_ru");
            entity.Property(e => e.GeneratedContentKk).HasColumnName("generated_content_kk");
            entity.Property(e => e.GeneratedContentEn).HasColumnName("generated_content_en");

            entity.Property(e => e.StudentSignedAt).HasColumnName("student_signed_at");
            entity.Property(e => e.EmployerSignedAt).HasColumnName("employer_signed_at");
            entity.Property(e => e.UniversitySignedAt).HasColumnName("university_signed_at");

            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.EndDate).HasColumnName("end_date");

            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.DeletedBy).HasColumnName("deleted_by");

            entity.HasOne(d => d.ContractTemplate)
                .WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ContractTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contracts_template");

            entity.HasOne(d => d.Application)
                .WithOne(p => p.Contract)
                .HasForeignKey<Contract>(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contracts_application");

            entity.HasOne(d => d.Status)
                .WithMany(p => p.Contracts)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contracts_status");
        });
        
        modelBuilder.Entity<ContractSignature>(entity =>
        {
            entity.HasKey(e => e.ContractSignatureId)
                .HasName("contract_signatures_pkey");

            entity.ToTable("contract_signatures");

            entity.Property(e => e.ContractSignatureId)
                .HasColumnName("contract_signature_id");

            entity.Property(e => e.ContractId)
                .HasColumnName("contract_id");

            entity.Property(e => e.SignerUserId)
                .HasColumnName("signer_user_id");

            entity.Property(e => e.SignerType)
                .HasMaxLength(50)
                .HasColumnName("signer_type");

            entity.Property(e => e.Lang)
                .HasMaxLength(10)
                .HasColumnName("lang");

            entity.Property(e => e.Signature)
                .HasColumnName("signature");

            entity.Property(e => e.SignedData)
                .HasColumnName("signed_data");

            entity.Property(e => e.SignedAt)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("signed_at");

            entity.HasOne(d => d.Contract)
                .WithMany(p => p.ContractSignatures)
                .HasForeignKey(d => d.ContractId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_contract_signatures_contract");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
