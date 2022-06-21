using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class SourceProductSearchPage : Page, IViewModel<SourceProductSearchViewModel>
    {
        public SourceProductSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SourceProductSearchViewModel>();
            DataContext = ViewModel;
        }

        public SourceProductSearchViewModel ViewModel { get; set; }
    }
}