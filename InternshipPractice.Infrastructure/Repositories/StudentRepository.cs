using InternshipPractice.Application.Interfaces.Repositories;
using InternshipPractice.Application.Responses;
using InternshipPractice.Infrastructure.Data;
using KDS.Primitives.FluentResult;
using Microsoft.EntityFrameworkCore;

namespace InternshipPractice.Infrastructure.Repositories;

public class StudentRepository(InternshipPracticeDbContext dbContext): IStudentRepository
{
    public async Task<Result<List<StudentResponse>>> GetAll()
    {
        try
        {
            var result = await dbContext.Students
                .Select(s => new StudentResponse
                {
                    UserId = s.UserId,
                    FirstName = s.User.FirstName,
                    LastName = s.User.LastName,
                    Course = s.Course,
                    Gpa = s.Gpa,
                    StatusName = s.Status.NameRu,
                    SkillsMap = s.StudentSkillMaps
                    .Select(ssm => ssm.Skill.NameRu)
                    .ToList()
                })
                .ToListAsync();

            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<List<StudentResponse>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<StudentResponse>>> GetStudentsByCareerUserId(Guid userId)
    {
        try
        {
            var students = await dbContext.Students
            .Where(s => dbContext.CareerCenters
                .Any(cc => cc.UniversityId == s.Faculty.UniversityId && cc.UserId == userId))
            .Select(s => new StudentResponse
            {
                UserId = s.UserId,
                FirstName = s.User.FirstName,
                LastName = s.User.LastName,
                Course = s.Course,
                Gpa = s.Gpa,
                StatusName = s.Status.NameRu,
                SkillsMap = s.StudentSkillMaps
                    .Select(ssm => ssm.Skill.NameRu)
                    .ToList()
            })
            .ToListAsync();

            return students;
        }
        catch (Exception ex)
        {
            return Result.Failure<List<StudentResponse>>(
            new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }

    public async Task<Result<List<CareerStudentApplicationResponse>>> GetStudentApplicationsByCareerUserId(Guid userId)
    {
        try
        {
            var applications = await dbContext.Applications
     .Where(a => dbContext.CareerCenters
         .Any(cc =>
             cc.UserId == userId &&
             cc.UniversityId == a.Student.Faculty.UniversityId))
     .Select(a => new
     {
         StudentId = a.Student.UserId,
         StudentLastName = a.Student.User.LastName,
         StudentFirstName = a.Student.User.FirstName,
         FacultyName = a.Student.Faculty.NameRu,
         CompanyName = a.Vacancy.Employer.Company.CompanyNameRu,
         StartDate = a.Vacancy.StartDate,
         EndDate = a.Vacancy.EndDate,
         StatusName = a.ApplicationStatus.NameRu,
         CreatedDate = a.CreatedAt
     })
     .ToListAsync();

            var result = applications
                .GroupBy(x => x.StudentId)
                .Select(g => g
                    .OrderByDescending(x => x.CreatedDate)
                    .First())
                .Select(a => new CareerStudentApplicationResponse
                {
                    StudentId = a.StudentId,
                    StudentFullName = $"{a.StudentLastName} {a.StudentFirstName}",
                    FacultyName = a.FacultyName,
                    CompanyName = a.CompanyName,
                    Period = $"{FormatDate(a.StartDate)} - {FormatDate(a.EndDate)}",
                    StatusName = a.StatusName
                })
                .ToList();

            return result;
        }
        catch (Exception ex)
        {
            return Result.Failure<List<CareerStudentApplicationResponse>>(
                new Error(Domain.Common.Error.InternalServerError, ex.Message));
        }
    }

    private static string FormatDate(DateOnly? date)
    {
        return date.HasValue
            ? date.Value.ToString("dd.MM.yyyy")
            : "-";
    }
}
