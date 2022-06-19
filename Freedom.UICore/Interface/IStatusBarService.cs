using Freedom.UICore.Models;
using System;

namespace Freedom.UICore.Interface
{
    public interface IStatusBarService
    {
        Action<string> DisplayUserName { get; set; }

        Action<string> DisplayCompanyName { get; set; }

        Action<string> DisplayShopName { get; set; }

        Action<string> DisplayBoxName { get; set; }

        Action<string> DisplayEmployee { get; set; }

        Action<string> DisplayMoney { get; set; }

        Action<StatusBarItem> DisplayAll { get; set; }
    }
}