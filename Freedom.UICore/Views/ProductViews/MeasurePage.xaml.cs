using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class MeasurePage : Page, IViewModel<MeasureViewModel>
    {
        public MeasurePage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<MeasureViewModel>();
        }

        public MeasureViewModel ViewModel { get; set; }
    }
}