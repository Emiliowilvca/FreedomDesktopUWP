using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class WayAdminSearchPage : Page, IViewModel<WayAdminSearchViewModel>
    {
        public WayAdminSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<WayAdminSearchViewModel>();
            DataContext = ViewModel;
        }

        public WayAdminSearchViewModel ViewModel { get; set; }
    }
}