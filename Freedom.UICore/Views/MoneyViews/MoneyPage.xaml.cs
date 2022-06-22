using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.EmployeeViews;
using Freedom.UICore.ViewModels.MainViews;
using Freedom.UICore.ViewModels.MoneyViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.MoneyViews
{
    public sealed partial class MoneyPage : Page, IViewModel<MoneyViewModel>
    {
        public MoneyPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<MoneyViewModel>();
        }

        public MoneyViewModel ViewModel { get; set; }
    }
}