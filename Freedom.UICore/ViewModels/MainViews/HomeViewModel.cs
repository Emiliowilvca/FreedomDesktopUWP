using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.MainViews
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            PageTitle = new PageTitle("HomePage", "", false);
        }
    }
}