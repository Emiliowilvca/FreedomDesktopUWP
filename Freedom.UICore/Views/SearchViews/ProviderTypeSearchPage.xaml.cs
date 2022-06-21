using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class ProviderTypeSearchPage : Page, IViewModel<ProviderTypeSearchViewModel>
    {
        public ProviderTypeSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ProviderTypeSearchViewModel>();
            DataContext = ViewModel;
        }

        public ProviderTypeSearchViewModel ViewModel { get; set; }
    }
}