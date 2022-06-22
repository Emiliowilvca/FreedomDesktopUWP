using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SaleViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SaleViews
{
    public sealed partial class SalePage : Page, IViewModel<SaleViewModel>
    {
        public SalePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SaleViewModel>();
        }

        public SaleViewModel ViewModel { get; set; }
    }
}