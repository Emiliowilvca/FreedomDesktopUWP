using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class JobPostSearchPage : Page, IViewModel<JobPostSearchViewModel>
    {
        public JobPostSearchPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<JobPostSearchViewModel>();
            DataContext = ViewModel;
        }

        public JobPostSearchViewModel ViewModel { get; set; }
    }
}