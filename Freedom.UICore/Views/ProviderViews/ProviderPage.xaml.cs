using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProviderViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ProviderViews
{
    public sealed partial class ProviderPage : Page, IViewModel<ProviderViewModel>
    {
        public ProviderViewModel ViewModel { get; set; }

        public ProviderPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetService<ProviderViewModel>();
            DataContext = ViewModel;
        }
    }
}