using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.EmployeeViews;
using Freedom.UICore.ViewModels.MainViews;
using Freedom.UICore.ViewModels.OperationViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.OperationViews
{
    public sealed partial class OperationTypePage : Page, IViewModel<OperationTypeViewModel>
    {
        public OperationTypePage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<OperationTypeViewModel>();
        }

        public OperationTypeViewModel ViewModel { get; set; }
    }
}