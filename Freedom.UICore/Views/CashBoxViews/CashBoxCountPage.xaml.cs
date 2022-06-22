using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.CashBoxViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CashBoxViews
{
    public sealed partial class CashBoxCountPage : Page, IViewModel<CashBoxCountViewModel>
    {
        public CashBoxCountPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CashBoxCountViewModel>();
        }

        public CashBoxCountViewModel ViewModel { get; set; }
    }
}