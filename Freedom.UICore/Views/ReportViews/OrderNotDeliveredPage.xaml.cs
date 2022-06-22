using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ReportViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ReportViews
{
    public sealed partial class OrderNotDeliveredPage : Page, IViewModel<OrderNotDeliveredViewModel>
    {
        public OrderNotDeliveredPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<OrderNotDeliveredViewModel>();
        }

        public OrderNotDeliveredViewModel ViewModel { get; set; }
    }
}