using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.MainViews;
using Microsoft.Extensions.DependencyInjection;

using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.MainViews
{
    public sealed partial class HomePage : Page, IViewModel<HomeViewModel>
    {
        public HomePage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<HomeViewModel>();
        }

        public HomeViewModel ViewModel { get; set; }
    }
}