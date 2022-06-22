using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class SubGroupPage : Page, IViewModel<SubGroupViewModel>
    {
        public SubGroupPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<SubGroupViewModel>();
        }

        public SubGroupViewModel ViewModel { get; set; }
    }
}