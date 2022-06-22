using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class SourceProductPage : Page, IViewModel<SourceProductViewModel>
    {
        public SourceProductPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SourceProductViewModel>();
        }

        public SourceProductViewModel ViewModel { get; set; }
    }
}