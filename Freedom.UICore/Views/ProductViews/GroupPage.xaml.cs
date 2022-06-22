using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.ProductViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;
namespace Freedom.UICore.Views.ProductViews
{
    public sealed partial class GroupPage : Page, IViewModel<GroupViewModel>
    {
        public GroupPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<GroupViewModel>();
        }

        public GroupViewModel ViewModel { get; set; }
    }
}