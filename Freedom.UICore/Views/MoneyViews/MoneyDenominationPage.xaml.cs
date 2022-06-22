using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.EmployeeViews;
using Freedom.UICore.ViewModels.MainViews;
using Freedom.UICore.ViewModels.MoneyViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.MoneyViews
{
    public sealed partial class MoneyDenominationPage : Page, IViewModel<MoneyDenominationViewModel>
    {
        public MoneyDenominationPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<MoneyDenominationViewModel>();
        }

        public MoneyDenominationViewModel ViewModel { get; set; }
    }
}