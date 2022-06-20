using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ShellViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ShellViews
{
    public sealed partial class NewUserPage : Page, IViewModel<NewUserViewModel>
    {
        public NewUserPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<NewUserViewModel>();
        }

        public NewUserViewModel ViewModel { get; set; }
    }
}