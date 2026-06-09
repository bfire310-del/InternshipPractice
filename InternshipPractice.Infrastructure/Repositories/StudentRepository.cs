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
}
