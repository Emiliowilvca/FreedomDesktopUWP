using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class CategorySearchPage : Page, IViewModel<CategorySearchViewModel>
    {
        public CategorySearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CategorySearchViewModel>();
            DataContext = ViewModel;
        }

        public CategorySearchViewModel ViewModel { get; set; }
    }
}