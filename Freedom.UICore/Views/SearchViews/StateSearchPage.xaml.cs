using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class StateSearchPage : Page, IViewModel<StateSearchViewModel>
    {
        public StateSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<StateSearchViewModel>();
            DataContext = ViewModel;
        }

        public StateSearchViewModel ViewModel { get; set; }
    }
}