using CommunityToolkit.Mvvm.Messaging;
using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.UICore.SendingMessages;
using Microsoft.Toolkit.Uwp.Helpers;
using System.Text.Json;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace Freedom.UICore.ViewModels.StatusBarViews
{
    public class StatusBarNotifyViewModel : BaseViewModel/*, IRecipient<NavigationMessage>*/
    {
        //private string _messageText;
        //private string _fontIconGlyph;
        //private SolidColorBrush _notifyBackground;
        //private SolidColorBrush _notifyForeground;

        public StatusBarNotifyViewModel()
        {
            //NotifyBackground = new SolidColorBrush(Colors.Gray);
            //_fontIconGlyph = MaterialDesignIcons.InformationOutline;
            //WeakReferenceMessenger.Default.Register<NavigationMessage>(this);
        }

        public void Receive(NavigationMessage message)
        {
            //NotifyModel notify = JsonSerializer.Deserialize<NotifyModel>(message.Value,
            //                                    new JsonSerializerOptions()
            //                                    {
            //                                        PropertyNameCaseInsensitive = true
            //                                    });

            //if (notify == null)
            //    return;

            //Color colorMessage = notify.NotifyBackground.ToColor();
            //Color foregroundColor = notify.NotifyForeground.ToColor();

            //FontIconGlyph = notify.FontIconGlyph;
            //MessageText = notify.Message;
            //NotifyBackground = new SolidColorBrush(colorMessage);
            //NotifyForeground = new SolidColorBrush(foregroundColor);
        }

        public override void OnNavigateTo(object parmeter)
        {
            base.OnNavigateTo(parmeter);

            //if (parmeter is NotifyModel notify)
            //{
            //    if (notify == null)
            //        return;

            //    Color colorMessage = notify.NotifyBackground.ToColor();
            //    Color foregroundColor = notify.NotifyForeground.ToColor();

            //    FontIconGlyph = notify.FontIconGlyph;
            //    MessageText = notify.Message;
            //    NotifyBackground = new SolidColorBrush(colorMessage);
            //    NotifyForeground = new SolidColorBrush(foregroundColor);
            //}
        }

        #region Properties

        //public string MessageText { get => _messageText; set => SetProperty(ref _messageText, value); }

        //public string FontIconGlyph { get => _fontIconGlyph; set => SetProperty(ref _fontIconGlyph, value); }

        //public SolidColorBrush NotifyBackground
        //{
        //    get => _notifyBackground;
        //    set => SetProperty(ref _notifyBackground, value);
        //}

        //public SolidColorBrush NotifyForeground
        //{
        //    get => _notifyForeground;
        //    set => SetProperty(ref _notifyForeground, value);
        //}

        #endregion Properties
    }
}