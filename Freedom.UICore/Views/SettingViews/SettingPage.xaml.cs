using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SettingViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SettingViews
{
    public sealed partial class SettingPage : Page, IViewModel<SettingViewModel>
    {
        public SettingPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SettingViewModel>();
        }

        public SettingViewModel ViewModel { get; set; }
    }
}