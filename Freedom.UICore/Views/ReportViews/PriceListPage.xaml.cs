using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class PriceListPage : Page, IViewModel<PriceListViewModel>
    {
        public PriceListPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PriceListViewModel>();
        }

        public PriceListViewModel ViewModel { get; set; }
    }
}