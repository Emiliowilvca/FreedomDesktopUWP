using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class DrugPage : Page, IViewModel<DrugViewModel>
    {
        public DrugPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<DrugViewModel>();
        }

        public DrugViewModel ViewModel { get; set; }
    }
}