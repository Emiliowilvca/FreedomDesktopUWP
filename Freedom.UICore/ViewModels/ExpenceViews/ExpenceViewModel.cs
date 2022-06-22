using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.Utility.Langs;

namespace Freedom.UICore.ViewModels.ExpenceViews
{
    public class ExpenceViewModel : BaseViewModel
    {
        public ExpenceViewModel()
        {
            PageTitle = new PageTitle(Lang.Expence, MaterialDesignIcons.Coffee);
        }
    }
}