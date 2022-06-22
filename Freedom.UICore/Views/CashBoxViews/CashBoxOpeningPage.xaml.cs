using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.CashBoxViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CashBoxViews
{
    public sealed partial class CashBoxOpeningPage : Page, IViewModel<CashBoxOpeningViewModel>
    {
        public CashBoxOpeningPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CashBoxOpeningViewModel>();
        }

        public CashBoxOpeningViewModel ViewModel { get; set; }
    }
}