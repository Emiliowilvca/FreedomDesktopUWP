using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class MeasureSearchPage : Page, IViewModel<MeasureSearchViewModel>
    {
        public MeasureSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<MeasureSearchViewModel>();
            DataContext = ViewModel;
        }

        public MeasureSearchViewModel ViewModel { get; set; }
    }
}