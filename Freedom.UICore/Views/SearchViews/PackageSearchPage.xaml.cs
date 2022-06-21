using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class PackageSearchPage : Page, IViewModel<PackageSearchViewModel>
    {
        public PackageSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PackageSearchViewModel>();
            DataContext = ViewModel;
        }

        public PackageSearchViewModel ViewModel { get; set; }
    }
}