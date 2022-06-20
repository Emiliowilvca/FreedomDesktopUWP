using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ShellViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ShellViews
{
    public sealed partial class ConfirmEmailPage : Page, IViewModel<ConfirmEmailViewModel>
    {
        public ConfirmEmailPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ConfirmEmailViewModel>();
        }

        public ConfirmEmailViewModel ViewModel { get; set; }
    }
}