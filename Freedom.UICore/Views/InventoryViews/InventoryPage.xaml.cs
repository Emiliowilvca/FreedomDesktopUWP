using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.EmployeeViews;
using Freedom.UICore.ViewModels.InventoryViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.InventoryViews
{
    public sealed partial class InventoryPage : Page, IViewModel<InventoryViewModel>
    {
        public InventoryPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<InventoryViewModel>();
        }

        public InventoryViewModel ViewModel { get; set; }
    }
}