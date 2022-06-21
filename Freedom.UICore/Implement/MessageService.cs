using Freedom.UICore.Interface;
using Freedom.Utility.Langs;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Implement
{
    public class MessageService : IMessageService
    {
        public async void ShowDialog(string message)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = "Save your work?";
            dialog.PrimaryButtonText = "Save";
            dialog.SecondaryButtonText = "Don't Save";
            dialog.CloseButtonText = "Cancel";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new object ();
            dialog.XamlRoot = Window.Current.Content.XamlRoot;

            var result = await dialog.ShowAsync();
        }

        public async void ShowMessage(string message, string title)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = title;
            dialog.CloseButtonText = Lang.Accept;
            dialog.Content = message;
            dialog.XamlRoot = Window.Current.Content.XamlRoot;
            //add the page for customize message
            //dialog.Content = new ContentDialogContent();
            var result = await dialog.ShowAsync();
        }

        public async Task<ContentDialogResult> ShowMessageConfirmation(string message, string title)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Title = title;
            dialog.PrimaryButtonText = Lang.Yes;
            dialog.SecondaryButtonText = Lang.No;
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = message;
            //add the page for customize message
            //dialog.Content = new ContentDialogContent();
            dialog.XamlRoot = Window.Current.Content.XamlRoot;
            
            
            return await dialog.ShowAsync();
        }
    }
}