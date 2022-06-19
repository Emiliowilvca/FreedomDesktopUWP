
using CommunityToolkit.Mvvm.Messaging.Messages;
using Freedom.UICore.Models;

namespace Freedom.UICore.SendingMessages
{
    public sealed class NavigationMessage : ValueChangedMessage<string>
    {
        public NavigationMessage(string value) : base(value)
        {
        }
    }

    public sealed class NavigationTitleMessage : ValueChangedMessage<PageTitle>
    {
        public NavigationTitleMessage(PageTitle pageTitle) : base(pageTitle)
        {
        }
    }
}