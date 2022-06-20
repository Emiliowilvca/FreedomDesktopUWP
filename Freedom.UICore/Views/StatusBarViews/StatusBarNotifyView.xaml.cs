using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Freedom.UICore.ViewModels.StatusBarViews;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Freedom.UICore.Views.StatusBarViews
{
    public sealed partial class StatusBarNotifyView : UserControl, IViewModel<StatusBarNotifyViewModel>
    {
        public StatusBarNotifyViewModel ViewModel { get; set; }

        public StatusBarNotifyView()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetService<StatusBarNotifyViewModel>();
        }
    }
}