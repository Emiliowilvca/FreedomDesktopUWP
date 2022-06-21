using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.StatusBarViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.StatusBarViews
{
    public sealed partial class StatusBarInfoPage : Page, IViewModel<StatusBarInfoViewModel>
    {
        public StatusBarInfoViewModel ViewModel { get; set; }

        public StatusBarInfoPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetService<StatusBarInfoViewModel>();
        }
    }
}