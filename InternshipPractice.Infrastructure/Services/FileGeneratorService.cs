using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using InternshipPractice.Application.Interfaces.Services;
using InternshipPractice.Application.Responses;

namespace InternshipPractice.Infrastructure.Services;

public class FileGeneratorService : IFileGeneratorService
{
    private const string DocxContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

    public FileResponse GenerateDocx(ContractDetailResponse contract)
    {
        var content = contract.ContractContent;

        if (string.IsNullOrWhiteSpace(content))
            throw new InvalidOperationException("Содержимое договора не найдено");

        var fileBytes = GenerateDocxBytes(contract, content);

        return new FileResponse
        {
            FileBytes = fileBytes,
            FileName = $"contract-{contract.ContractNumber ?? contract.ContractId.ToString()}.docx",
            ContentType = DocxContentType
        };
    }

    private static byte[] GenerateDocxBytes(ContractDetailResponse contract, string content)
    {
        using var stream = new MemoryStream();

        using (var document = WordprocessingDocument.Create(
                   stream,
                   WordprocessingDocumentType.Document,
                   true))
        {
            var mainPart = document.AddMainDocumentPart();
            mainPart.Document = new Document();

            var body = new Body();

            body.Append(CreateTitle($"Договор №{contract.ContractNumber ?? contract.ContractId.ToString()}"));
            body.Append(CreateParagraph($"Практика: {contract.JobTitle ?? "—"}"));
            body.Append(CreateParagraph($"Студент: {contract.Student ?? "—"}"));
            body.Append(CreateParagraph($"Работодатель: {contract.Company ?? "—"}"));
            body.Append(CreateParagraph($"Университет: {contract.University ?? "—"}"));
            body.Append(CreateParagraph($"Период: {FormatDate(contract.StartDate)} - {FormatDate(contract.EndDate)}"));
            body.Append(CreateEmptyParagraph());

            foreach (var line in content.Split('\n'))
                body.Append(CreateParagraph(line.TrimEnd()));

            mainPart.Document.Append(body);
            mainPart.Document.Save();
        }

        return stream.ToArray();
    }

    private static Paragraph CreateTitle(string text)
    {
        return new Paragraph(
            new ParagraphProperties(
                new Justification { Val = JustificationValues.Center },
                new SpacingBetweenLines { After = "240" }),
            new Run(
                new RunProperties(
                    new Bold(),
                    new FontSize { Val = "28" }),
                new Text(text)));
    }

    private static Paragraph CreateParagraph(string text)
    {
        return new Paragraph(
            new ParagraphProperties(
                new SpacingBetweenLines { After = "120" }),
            new Run(
                new RunProperties(
                    new FontSize { Val = "24" }),
                new Text(text)
                {
                    Space = SpaceProcessingModeValues.Preserve
                }));
    }

    private static Paragraph CreateEmptyParagraph()
    {
        return new Paragraph(
            new Run(
                new Text("")));
    }

    private static string FormatDate(DateOnly? date)
    {
        return date?.ToString("dd.MM.yyyy") ?? "—";
    }
}
