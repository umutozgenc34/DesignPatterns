using DesignPatterns.Observer.Models;
using System.Net.Mail;
using System.Net;

namespace DesignPatterns.Observer.Observer;

public class UserObserverSendEmail : IUserObserver
{
    private readonly IServiceProvider _serviceProvider;

    public UserObserverSendEmail(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void UserCreated(AppUser appUser)
    {
        var logger = _serviceProvider.GetRequiredService<ILogger<UserObserverSendEmail>>();

        var mailMessage = new MailMessage();

        var smptClient = new SmtpClient("mx5.fastmailsvc.com");

        mailMessage.From = new MailAddress("umutozgenc34@gmail.com");

        mailMessage.To.Add(new MailAddress(appUser.Email));

        mailMessage.Subject = "Hi there,Welcome to MySite";

        mailMessage.Body = "<p>Since you're a new member, you've earned a 10% discount!</p>";

        mailMessage.IsBodyHtml = true;
        smptClient.Port = 587;
        smptClient.Credentials = new NetworkCredential("umutozgenc34@gmail.com", "Sifre34.");

        smptClient.Send(mailMessage);
        logger.LogInformation($"Email was send to user :{appUser.UserName}");
    }
}
