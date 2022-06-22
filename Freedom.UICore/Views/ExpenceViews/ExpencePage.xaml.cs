using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.EmployeeViews;
using Freedom.UICore.ViewModels.ExpenceViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ExpenceViews
{
    public sealed partial class ExpencePage : Page, IViewModel<ExpenceViewModel>
    {
        public ExpencePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ExpenceViewModel>();
        }

        public ExpenceViewModel ViewModel { get; set; }
    }
}