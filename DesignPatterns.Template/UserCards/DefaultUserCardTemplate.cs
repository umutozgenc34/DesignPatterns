namespace DesignPatterns.Template.UserCards;

public class DefaultUserCardTemplate : UserCardTemplate
{
    protected override string SetFooter()
    {
        return string.Empty;
    }

    protected override string SetImage()
    {
        return $"<img class='card-img-top' src='/userpictures/unauth.png'>";
    }
}
