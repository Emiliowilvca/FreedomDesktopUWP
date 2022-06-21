using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class SectorProductSearchPage : Page, IViewModel<SectorProductSearchViewModel>
    {
        public SectorProductSearchPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SectorProductSearchViewModel>();
            DataContext = ViewModel;
        }

        public SectorProductSearchViewModel ViewModel { get; set; }
    }
}