using Freedom.UICore.BaseClass;
using Freedom.UICore.ViewModels.SearchViews;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml.Controls;

namespace Freedom.UICore.Views.SearchViews
{
    public sealed partial class GroupSearchPage : Page, IViewModel<GroupSearchViewModel>
    {
        public GroupSearchPage()
        {
            this.InitializeComponent();
            ViewModel = AppEssential.ServiceProvider.GetRequiredService<GroupSearchViewModel>();
            DataContext = ViewModel;
        }

        public GroupSearchViewModel ViewModel { get; set; }
    }
}