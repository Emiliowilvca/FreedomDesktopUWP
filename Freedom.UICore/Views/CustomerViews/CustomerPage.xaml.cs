using Freedom.UICore.BaseClass;
//using Freedom.UICore.ViewModels.AccountViews;
using Freedom.UICore.ViewModels.CustomerViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CustomerViews
{
    public sealed partial class CustomerPage : Page, IViewModel<CustomerViewModel>
    {
        public CustomerViewModel ViewModel { get; set; }

        public CustomerPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetService<CustomerViewModel>();
        }
    }
}