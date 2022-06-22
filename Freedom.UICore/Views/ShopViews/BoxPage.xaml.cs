using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Freedom.UICore.ViewModels.ShopViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ShopViews
{
    public sealed partial class BoxPage : Page, IViewModel<BoxViewModel>
    {
        public BoxPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BoxViewModel>();
        }

        public BoxViewModel ViewModel { get; set; }
    }
}