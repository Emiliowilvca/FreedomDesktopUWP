using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class WayAdminPage : Page, IViewModel<WayAdminViewModel>
    {
        public WayAdminPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<WayAdminViewModel>();
        }

        public WayAdminViewModel ViewModel { get; set; }
    }
}