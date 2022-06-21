using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class CustomerAccountSearchPage : Page, IViewModel<CustomerAccountSearchViewModel>
    {
        public CustomerAccountSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CustomerAccountSearchViewModel>();
            DataContext = ViewModel;
        }

        public CustomerAccountSearchViewModel ViewModel { get; set; }
    }
}