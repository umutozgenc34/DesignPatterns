using DesignPatterns.Observer.Models;

namespace DesignPatterns.Observer.Observer;

public interface IUserObserver
{
    void UserCreated(AppUser user);
}
