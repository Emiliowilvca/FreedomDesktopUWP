using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SettingViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.Setting
{
    public sealed partial class SettingPage : Page, IViewModel<SettingViewModel>
    {
        public SettingViewModel ViewModel { get; set; }

        public SettingPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetService<SettingViewModel>();
        }
    }
}