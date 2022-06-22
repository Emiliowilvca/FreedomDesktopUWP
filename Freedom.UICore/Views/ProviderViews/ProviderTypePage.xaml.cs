using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProviderViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProviderViews
{
    public sealed partial class ProviderTypePage : Page, IViewModel<ProviderTypeViewModel>
    {
        public ProviderTypePage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ProviderTypeViewModel>();
        }

        public ProviderTypeViewModel ViewModel { get; set; }
    }
}