using DesignPatterns.Observer.Events;
using MediatR;

namespace DesignPatterns.Observer.EventHandlers;

public class CreatedUserWriteConsoleEventHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly ILogger<CreatedUserWriteConsoleEventHandler> _logger;

    public CreatedUserWriteConsoleEventHandler(ILogger<CreatedUserWriteConsoleEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"user created : Id= {notification.AppUser.Id}");

        return Task.CompletedTask;
    }
}