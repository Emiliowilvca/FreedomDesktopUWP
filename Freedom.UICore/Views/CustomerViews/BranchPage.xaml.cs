using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.CustomerViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.CustomerViews
{
    public sealed partial class BranchPage : Page, IViewModel<BranchViewModel>
    {
        public BranchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<BranchViewModel>();
        }

        public BranchViewModel ViewModel { get; set; }
    }
}