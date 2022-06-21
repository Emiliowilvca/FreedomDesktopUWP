using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class BrandSearchPage : Page, IViewModel<BrandSearchViewModel>
    {
        public BrandSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BrandSearchViewModel>();
            DataContext = ViewModel;
        }

        public BrandSearchViewModel ViewModel { get; set; }
    }
}