using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
     
    public sealed partial class ProviderSearchPage : Page, IViewModel<ProviderSearchViewModel>
    {
        public ProviderSearchPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ProviderSearchViewModel>();
        }

        public ProviderSearchViewModel ViewModel { get; set; }
    }
}
