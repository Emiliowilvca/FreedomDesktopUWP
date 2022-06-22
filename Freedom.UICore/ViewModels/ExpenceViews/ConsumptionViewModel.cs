using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.Utility.Langs;

namespace Freedom.UICore.ViewModels.ExpenceViews
{
    public class ConsumptionViewModel : BaseViewModel
    {
        public ConsumptionViewModel()
        {
            Title = Lang.Consumption;
            PageTitle = new PageTitle(Lang.Consumption, MaterialDesignIcons.FoodForkDrink);
        }
    }
}