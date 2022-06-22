using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.EmployeeViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.EmployeeViews
{
    public sealed partial class EmployeePage : Page, IViewModel<EmployeeViewModel>
    {
        public EmployeePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<EmployeeViewModel>();
        }

        public EmployeeViewModel ViewModel { get; set; }
    }
}