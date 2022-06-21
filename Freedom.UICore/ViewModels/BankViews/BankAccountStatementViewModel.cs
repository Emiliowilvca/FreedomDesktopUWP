using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;

namespace Freedom.UICore.ViewModels.BankViews
{
    public class BankAccountStatementViewModel : BaseViewModel
    {
        public BankAccountStatementViewModel()
        {
            PageTitle = new Models.PageTitle(Lang.AccountStatement, MaterialDesignIcons.ChartBoxOutline);
        }
    }
}