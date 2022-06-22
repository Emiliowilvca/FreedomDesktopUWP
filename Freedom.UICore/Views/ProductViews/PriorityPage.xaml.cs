using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class PriorityPage : Page, IViewModel<PriorityViewModel>
    {
        public PriorityPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<PriorityViewModel>();
        }

        public PriorityViewModel ViewModel { get; set; }
    }
}