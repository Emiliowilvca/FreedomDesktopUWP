using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class SectorProductPage : Page, IViewModel<SectorProductViewModel>
    {
        public SectorProductPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SectorProductViewModel>();
        }

        public SectorProductViewModel ViewModel { get; set; }
    }
}