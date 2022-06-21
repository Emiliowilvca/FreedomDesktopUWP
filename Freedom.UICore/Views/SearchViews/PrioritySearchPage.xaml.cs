using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class PrioritySearchPage : Page, IViewModel<PrioritySearchViewModel>
    {
        public PrioritySearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PrioritySearchViewModel>();
            DataContext = ViewModel;
        }

        public PrioritySearchViewModel ViewModel { get; set; }
    }
}