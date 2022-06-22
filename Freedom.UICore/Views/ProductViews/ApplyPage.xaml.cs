using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class ApplyPage : Page, IViewModel<ApplyViewModel>
    {
        public ApplyPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<ApplyViewModel>();
        }

        public ApplyViewModel ViewModel { get; set; }
    }
}