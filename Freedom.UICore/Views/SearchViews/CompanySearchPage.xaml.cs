using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class CompanySearchPage : Page, IViewModel<CompanySearchViewModel>
    {
        public CompanySearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CompanySearchViewModel>();
            DataContext = ViewModel;
        }

        public CompanySearchViewModel ViewModel { get; set; }
    }
}