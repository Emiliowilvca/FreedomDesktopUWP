using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;

namespace Freedom.UICore.ViewModels.StatusBarViews
{
    public class StatusBarInfoViewModel : BaseViewModel
    {
        private StatusBarItem _statusBarItem;

        public StatusBarItem StatusBarItem { get => _statusBarItem; set => _statusBarItem = value; }

        public StatusBarInfoViewModel()
        {
            StatusBarItem = new StatusBarItem();

            //_statusBarService.DisplayUserName = (x) => { StatusBarItem.UserEmail = x; };
            //_statusBarService.DisplayCompanyName = (x) => { StatusBarItem.CompanyName = x; };
            //_statusBarService.DisplayShopName = (x) => { StatusBarItem.ShopName = x; };
            //_statusBarService.DisplayBoxName = (x) => { StatusBarItem.BoxName = x; };
            //_statusBarService.DisplayEmployee = (x) => { StatusBarItem.EmployeeName = x; };
            //_statusBarService.DisplayMoney = (x) => { StatusBarItem.MoneyName = x; };

            //_statusBarService.DisplayAll = (item) =>
            //{
            //    StatusBarItem.UserEmail = item.UserEmail;
            //    StatusBarItem.CompanyName = item.CompanyName;
            //    StatusBarItem.ShopName = item.ShopName;
            //    StatusBarItem.BoxName = item.BoxName;
            //    StatusBarItem.EmployeeName = item.EmployeeName;
            //    StatusBarItem.MoneyName = item.MoneyName;
            //};
        }
    }
}