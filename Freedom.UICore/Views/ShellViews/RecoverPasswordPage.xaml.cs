using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ShellViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ShellViews
{
    public sealed partial class RecoverPasswordPage : Page, IViewModel<RecoverPasswordViewModel>
    {
        public RecoverPasswordPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<RecoverPasswordViewModel>();
        }

        public RecoverPasswordViewModel ViewModel { get; set; }
    }
}