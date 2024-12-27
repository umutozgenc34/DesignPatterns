using DesignPatterns.Template.Models;
using System.Text;

namespace DesignPatterns.Template.UserCards;

public abstract class UserCardTemplate
{
    protected AppUser AppUser { get; set; }

    public void SetUser(AppUser user)
    {
        AppUser = user;
    }

    public string Build()
    {
        if(AppUser is null) throw new ArgumentNullException(nameof(AppUser));

        var sb = new StringBuilder();

        sb.Append("<div class='card'>");
        sb.Append(SetImage());
        sb.Append($@"<div class='card-body'>
                          <h5>{AppUser.UserName}</h5>
                          <p>{AppUser.Description}</p>");
        sb.Append(SetFooter());
        sb.Append("</div>");
        sb.Append("</div>");

        return sb.ToString();

    }

    protected abstract string SetFooter();
    protected abstract string SetImage();
}
