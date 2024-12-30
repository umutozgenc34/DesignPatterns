using DesignPatterns.Observer.Events;
using MediatR;
using System.Net.Mail;
using System.Net;

namespace DesignPatterns.Observer.EventHandlers;

public class SendEmailEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly ILogger<SendEmailEventHandler> _logger;

    public SendEmailEventHandler(ILogger<SendEmailEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var mailMessage = new MailMessage();

        var smptClient = new SmtpClient("mx5.fastmailsvc.com");

        mailMessage.From = new MailAddress("umutozgenc34@gmail.com");

        mailMessage.To.Add(new MailAddress(notification.AppUser.Email));

        mailMessage.Subject = "Hi there,Welcome to MySite";

        mailMessage.Body = "<p>Since you're a new member, you've earned a 10% discount!</p>";

        mailMessage.IsBodyHtml = true;
        smptClient.Port = 587;
        smptClient.Credentials = new NetworkCredential("umutozgenc34@gmail.com", "Sifre34.");

        smptClient.Send(mailMessage);
        _logger.LogInformation($"Email was send to user :{notification.AppUser.UserName}");

        return Task.CompletedTask;
    }
}
