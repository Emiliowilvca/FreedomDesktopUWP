using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class EmployeeSearchPage : Page, IViewModel<EmployeeSearchViewModel>
    {
        public EmployeeSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<EmployeeSearchViewModel>();
            DataContext = ViewModel;
        }

        public EmployeeSearchViewModel ViewModel { get; set; }
    }
}