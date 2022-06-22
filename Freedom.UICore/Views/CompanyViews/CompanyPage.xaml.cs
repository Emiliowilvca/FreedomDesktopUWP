using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.CompanyViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CompanyViews
{
    public sealed partial class CompanyPage : Page, IViewModel<CompanyViewModel>
    {
        public CompanyPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CompanyViewModel>();
        }

        public CompanyViewModel ViewModel { get; set; }
    }
}