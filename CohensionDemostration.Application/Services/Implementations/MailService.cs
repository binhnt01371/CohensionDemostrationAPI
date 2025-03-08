using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace CohensionDemostration.Application.Services;

public class MailService : IMailService
{
    public IConfiguration _configuration { get; set; }
    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task SendNotificationEmailAsync(string to, string subject, string body)
    {
        try
        {
            var client = new SmtpClient("SmtpClientHost") { 
                Port = 587,
                Credentials = new NetworkCredential(_configuration["SmtpClientAccount"], _configuration["SmtpClientPassword"]),
                EnableSsl = true
            };

            var emailMessage = new MailMessage()
            {
                From = new MailAddress(to),
                Subject = subject,
                Body = body,
                IsBodyHtml = false
            };
            emailMessage.To.Add(new MailAddress(to));
            client.Send(emailMessage);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {

            throw ex;
        }
    }
}
