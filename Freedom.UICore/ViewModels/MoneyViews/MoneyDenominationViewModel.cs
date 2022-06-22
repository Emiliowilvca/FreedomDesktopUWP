using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;
namespace Freedom.UICore.ViewModels.MoneyViews
{
    public class MoneyDenominationViewModel : BaseViewModel
    {
        public MoneyDenominationViewModel()
        {
            Title = Lang.BanknotesDenomination;
            PageTitle = new PageTitle(Lang.BanknotesDenomination, MaterialDesignIcons.Cash100);
        }
    }
}