using DesignPatterns.Observer.Models;
using MediatR;

namespace DesignPatterns.Observer.Events;

public class UserCreatedEvent : INotification
{
    public AppUser AppUser { get; set; }
}
