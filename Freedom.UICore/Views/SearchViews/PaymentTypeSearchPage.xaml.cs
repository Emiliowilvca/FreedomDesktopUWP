using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class PaymentTypeSearchPage : Page, IViewModel<PaymentTypeSearchViewModel>
    {
        public PaymentTypeSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PaymentTypeSearchViewModel>();
            DataContext = ViewModel;
        }

        public PaymentTypeSearchViewModel ViewModel { get; set; }
    }
}