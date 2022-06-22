using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.EmployeeViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.EmployeeViews
{
    public sealed partial class JobPostPage : Page, IViewModel<JobPostViewModel>
    {
        public JobPostPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<JobPostViewModel>();
        }

        public JobPostViewModel ViewModel { get; set; }
    }
}