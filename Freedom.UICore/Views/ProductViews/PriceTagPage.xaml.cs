using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class PriceTagPage : Page, IViewModel<PriceTagViewModel>
    {
        public PriceTagPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PriceTagViewModel>();
        }

        public PriceTagViewModel ViewModel { get; set; }
    }
}