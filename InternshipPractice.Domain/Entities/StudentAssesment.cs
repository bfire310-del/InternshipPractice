using System;
using System.Collections.Generic;

namespace InternshipPractice.Infrastructure.Entities;

public partial class StudentAssesment
{
    public Guid StudentAssesmentId { get; set; }

    public Guid? StudentId { get; set; }

    public Guid? VacancyId { get; set; }

    public int? PunctuallyAndDiscipline { get; set; }

    public string? PunctuallyAndDisciplineComment { get; set; }

    public int? ProfessionalKnowledge { get; set; }

    public string? ProfessionalKnowledgeComment { get; set; }

    public int? QualityOfTheAssignedTasks { get; set; }

    public string? QualityOfTheAssignedTasksComment { get; set; }

    public int? InitiativeAndIndependence { get; set; }

    public string? InitiativeAndIndependenceComment { get; set; }

    public int? TeamworkAndCommunication { get; set; }

    public string? TeamworkAndCommunicationComment { get; set; }

    public int? OverallFinalScore { get; set; }

    public string? OverallFinalScoreComment { get; set; }

    public string? FinalReview { get; set; }

    public Guid? StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public Guid? DeletedBy { get; set; }

    public virtual StudentAssesmentStatus? Status { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Vacancy? Vacancy { get; set; }
}
