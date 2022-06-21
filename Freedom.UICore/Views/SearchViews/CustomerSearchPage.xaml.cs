using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class CustomerSearchPage : Page, IViewModel<CustomerSearchViewModel>
    {
        public CustomerSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CustomerSearchViewModel>();
            DataContext = ViewModel;
        }

        public CustomerSearchViewModel ViewModel { get; set; }
    }
}