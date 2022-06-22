using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.CustomerViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CustomerViews
{
    public sealed partial class CustomerAccountPage : Page, IViewModel<CustomerAccountViewModel>
    {
        public CustomerAccountPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CustomerAccountViewModel>();
        }

        public CustomerAccountViewModel ViewModel { get; set; }
    }
}