using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.FeesViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.FeesViews
{
    public sealed partial class FeesGeneratorPage : Page, IViewModel<FeesGeneratorViewModel>
    {
        public FeesGeneratorPage()
        {
            this.InitializeComponent();

            ViewModel = AppEssential.ServiceProvider.GetRequiredService<FeesGeneratorViewModel>();
        }

        public FeesGeneratorViewModel ViewModel { get; set; }
    }
}