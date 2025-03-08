namespace CohensionDemostration.Application.Services;

public interface IMailService
{
    public Task SendNotificationEmailAsync(string to, string subject, string body);
}
