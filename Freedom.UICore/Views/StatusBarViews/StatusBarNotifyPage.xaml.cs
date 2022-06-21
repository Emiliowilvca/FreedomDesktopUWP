using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.StatusBarViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.StatusBarViews
{
    public sealed partial class StatusBarNotifyPage : Page, IViewModel<StatusBarNotifyViewModel>
    {
        public StatusBarNotifyPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<StatusBarNotifyViewModel>();
        }

        public StatusBarNotifyViewModel ViewModel { get; set; }
    }
}