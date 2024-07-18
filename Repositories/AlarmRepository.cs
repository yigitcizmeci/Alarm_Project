using System.Security.Claims;
using System.Text;
using Alarm_Project.DTOs;
using Alarm_Project.Models;
using Alarm_Project.Repositories.Contracts;
using Alarm_Project.Repositories.DbRepo;
using Alarm_Project.Services;
using Alarm_Project.Services.Contracts;
using AutoMapper;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Slack.Webhooks;

namespace Alarm_Project.Repositories;

public class AlarmRepository(
    RepositoryContext repositoryContext,
    AlarmSettings alarmSettings,
    Alarm alarm,
    Users users,
    IMapper mapper,
    SlackService slackService,
    IHttpContextAccessor httpContextAccessor,
    IEmailSender emailSender) : Controller,IAlarmRepository<Alarm>, IAlarmSettingsRepository<AlarmSettings>
{
    public async Task<Alarm> Alarm(Alarm alarm)
    {
        var Identity = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        var UserIdClaim = Identity.FindFirst(ClaimTypes.NameIdentifier).Value;
        alarm = new Alarm()
        {
            UserId = new Guid(UserIdClaim),
            AlarmId = alarm.AlarmId,
            AlarmType = alarm.AlarmType,
            AlarmMessage = alarm.AlarmMessage,
            AlarmSettings = alarm.AlarmSettings,
            Time = DateTime.Now
        };
        var result = await Task.FromResult(alarm);
        await repositoryContext.AddAsync(result);
        await repositoryContext.SaveChangesAsync();
        return result;
    }
    
    public async Task<string> SendReport(string alarmMessage)
    {
        var fileName = $"AlarmReport_{DateTime.Now:yyyyMMddHHmmss}.pdf";
        var filePath = Path.Combine("Reports", fileName);

        if (!Directory.Exists("Reports"))
        {
            Directory.CreateDirectory("Reports");
        }

        var writer = new PdfWriter(filePath);
        var pdf = new PdfDocument(writer);
        var document = new Document(pdf);

        // Font ayarları
        var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
        var font2 = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
        var titleFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLDOBLIQUE);

        // Başlık
        var title = new Paragraph("Alarm Report")
            .SetFont(titleFont)
            .SetFontSize(20)
            .SetBold()
            .SetTextAlignment(TextAlignment.CENTER);
        document.Add(title);

        //Alt başlık
        var subTitle = new Paragraph("===============================================================")
            .SetFont(font)
            .SetFontSize(12)
            .SetTextAlignment(TextAlignment.CENTER);
        document.Add(subTitle);

        //Mesaj
        var message = new Paragraph($"Message: {alarmMessage}")
            .SetFont(font)
            .SetFontSize(14)
            .SetMarginTop(20);
        document.Add(message);

        //Tarih
        var date = new Paragraph($"Date: {DateTime.Now}")
            .SetFont(font)
            .SetFontSize(14)
            .SetMarginTop(10);
        document.Add(date);

        //Tablo 
        var table = new Table(2, false);
        table.AddCell(new Cell().Add(new Paragraph("Key").SetBold()));
        table.AddCell(new Cell().Add(new Paragraph("Value").SetBold()));

        table.AddCell(new Cell().Add(new Paragraph("Message")))
            .SetFont(font2)
            .SetFontSize(11);
        table.AddCell(new Cell().Add(new Paragraph(alarmMessage)))
            .SetFont(font2)
            .SetFontSize(11);
        table.AddCell(new Cell().Add(new Paragraph("Date")));
        table.AddCell(new Cell().Add(new Paragraph(DateTime.Now.ToString())));

        document.Add(table);

        // logo
        var logoPath = @"C:\Users\yigit\OneDrive\Masaüstü\logo.png"; 
        if (System.IO.File.Exists(logoPath))
        {
            ImageData imageData = ImageDataFactory.Create(logoPath);
            var image = new Image(imageData)
                .ScaleToFit(400, 400)
                .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                .SetAutoScale(true)
                .SetOpacity(0.5f); 
            document.Add(image);
        }

        document.Close();
        return filePath;
        // var reportContent = GenerateReport(alarmMessage);
        // var fileName = $"AlarmReport_{DateTime.Now:yyyyMMddHHmmss}.txt";
        // var filePath = Path.Combine("Reports", fileName);
        //
        // if (!Directory.Exists("Reports"))
        // {
        //     Directory.CreateDirectory("Reports");
        // }
        //
        // await System.IO.File.WriteAllTextAsync(filePath, reportContent, Encoding.UTF8);
        // return filePath;
    }

    private string GenerateReport(string message)
    {
        var report = new StringBuilder();
        report.AppendLine("===== Alarm DOC =====");
        report.AppendLine("======================");
        report.AppendLine($"Message: {message}");
        report.AppendLine($"Date: {DateTime.Now}");
        return report.ToString();
    }

    public async Task<bool> SendEmailAsync(string AlarmMessage)
    {
        var EmailClaim = httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
        var userEmail = EmailClaim?.FindFirst(ClaimTypes.Email);
        var userIdClaim = EmailClaim.FindFirst(ClaimTypes.NameIdentifier).Value;
        await SendReport(AlarmMessage);

        if (userEmail == null)
        {
            throw new ArgumentNullException("User Email claim in not present");
        }

        var receiver = userEmail.ToString();
        var subject = "Alarm Notification";
        var message = AlarmMessage;
        var filePath = await SendReport(AlarmMessage);

        await emailSender.SendEmailAsync(receiver, subject, message, filePath);
        return true;
    }

    public async Task<bool> SendSlackAsync(string message)
    {
        return await slackService.SendSlackMessage(message);
    }
}