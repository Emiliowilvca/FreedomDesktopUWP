using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReturnViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ReturnViews
{
    public sealed partial class SaleReturnPage : Page, IViewModel<SaleReturnViewModel>
    {
        public SaleReturnPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SaleReturnViewModel>();
        }

        public SaleReturnViewModel ViewModel { get; set; }
    }
}