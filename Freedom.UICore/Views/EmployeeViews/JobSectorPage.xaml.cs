using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.EmployeeViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.EmployeeViews
{
    public sealed partial class JobSectorPage : Page, IViewModel<JobSectorViewModel>
    {
        public JobSectorPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<JobSectorViewModel>();
        }

        public JobSectorViewModel ViewModel { get; set; }
    }
}