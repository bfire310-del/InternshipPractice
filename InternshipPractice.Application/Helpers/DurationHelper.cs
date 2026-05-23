namespace InternshipPractice.Application.Helpers;

public static class DurationHelper
{
    public static string? CalculateDurationText(DateOnly? startDate, DateOnly? endDate)
    {
        if (startDate is null || endDate is null)
            return null;

        if (endDate < startDate)
            return null;

        var months = (endDate.Value.Year - startDate.Value.Year) * 12
            + endDate.Value.Month - startDate.Value.Month;

        if (endDate.Value.Day < startDate.Value.Day)
            months--;

        if (months <= 0)
            return "До 1 месяца";

        if (months == 1)
            return "1 месяц";

        if (months is >= 2 and <= 4)
            return $"{months} месяца";

        if (months is >= 5 and <= 11)
            return $"{months} месяцев";

        var years = months / 12;
        var restMonths = months % 12;

        if (years == 1 && restMonths == 0)
            return "1 год";

        if (restMonths == 0)
            return $"{years} года";

        return $"{years} г. {restMonths} мес.";
    }
}