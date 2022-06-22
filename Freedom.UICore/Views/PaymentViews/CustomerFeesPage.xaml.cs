using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.EmployeeViews;
using Freedom.UICore.ViewModels.MainViews;
using Freedom.UICore.ViewModels.PaymentViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.PaymentViews
{
    public sealed partial class CustomerFeesPage : Page, IViewModel<CustomerFeesViewModel>
    {
        public CustomerFeesPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CustomerFeesViewModel>();
        }

        public CustomerFeesViewModel ViewModel { get; set; }
    }
}