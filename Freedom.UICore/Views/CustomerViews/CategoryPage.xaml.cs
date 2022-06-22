using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.CustomerViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CustomerViews
{
    public sealed partial class CategoryPage : Page, IViewModel<CategoryViewModel>
    {
        public CategoryPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<CategoryViewModel>();
        }

        public CategoryViewModel ViewModel { get; set; }
    }
}