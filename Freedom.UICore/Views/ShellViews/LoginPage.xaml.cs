using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ShellViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ShellViews
{
    public sealed partial class LoginPage : Page, IViewModel<LoginViewModel>
    {
        public LoginPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<LoginViewModel>();
        }

        public LoginViewModel ViewModel { get; set; }
    }
}