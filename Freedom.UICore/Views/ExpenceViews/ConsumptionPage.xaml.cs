using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ExpenceViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ExpenceViews
{
    public sealed partial class ConsumptionPage : Page, IViewModel<ConsumptionViewModel>
    {
        public ConsumptionPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ConsumptionViewModel>();
        }

        public ConsumptionViewModel ViewModel { get; set; }
    }
}