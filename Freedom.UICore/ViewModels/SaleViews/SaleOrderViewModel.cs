﻿using Freedom.Frontend.FontIcons;
using Freedom.UICore.BaseClass;
using Freedom.Utility.Langs;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.SaleViews
{
    public class SaleOrderViewModel : BaseViewModel
    {
        public SaleOrderViewModel()
        {
            PageTitle = new PageTitle(Lang.SalesOrder, MaterialDesignIcons.CartArrowDown);
        }
    }
}