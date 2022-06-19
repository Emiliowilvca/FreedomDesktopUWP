using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Interface
{
    public interface IMessageService
    {
        void ShowMessage(string message, string title);

        Task<ContentDialogResult> ShowMessageConfirmation(string message, string title);
    }
}