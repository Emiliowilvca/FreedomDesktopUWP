using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class JobSectorSearchPage : Page, IViewModel<JobSectorSearchViewModel>
    {
        public JobSectorSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<JobSectorSearchViewModel>();
            DataContext = ViewModel;
        }

        public JobSectorSearchViewModel ViewModel { get; set; }
    }
}