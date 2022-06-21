using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class ApplySearchPage : Page, IViewModel<ApplySearchViewModel>
    {
        public ApplySearchPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ApplySearchViewModel>();
            DataContext = ViewModel;
        }

        public ApplySearchViewModel ViewModel { get; set; }
    }
}