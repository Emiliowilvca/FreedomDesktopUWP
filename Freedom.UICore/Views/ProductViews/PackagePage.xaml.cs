using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class PackagePage : Page, IViewModel<PackageViewModel>
    {
        public PackagePage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PackageViewModel>();
        }

        public PackageViewModel ViewModel { get; set; }
    }
}