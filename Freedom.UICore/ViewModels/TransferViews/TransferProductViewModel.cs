using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.TransferViews
{
    public class TransferProductViewModel : BaseViewModel
    {
        public TransferProductViewModel()
        {
            Title = Lang.TransferProduct;
            PageTitle = new PageTitle(Lang.Transfers, MaterialDesignIcons.Transfer);
        }
    }
}