using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class SubGroupSearchPage : Page, IViewModel<SubGroupSearchViewModel>
    {
        public SubGroupSearchPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SubGroupSearchViewModel>();
            DataContext = ViewModel;
        }

        public SubGroupSearchViewModel ViewModel { get; set; }
    }
}