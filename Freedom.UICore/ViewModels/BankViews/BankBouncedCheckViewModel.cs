using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.Utility.Langs;

namespace Freedom.UICore.ViewModels.BankViews
{
    public class BankBouncedCheckViewModel : BaseViewModel
    {
        public BankBouncedCheckViewModel()
        {
            Title = Lang.BouncedCheck;
            PageTitle = new PageTitle(Lang.BouncedCheck, MaterialDesignIcons.Bank);
        }
    }
}