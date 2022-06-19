using Freedom.UICore.Interface;
using Freedom.UICore.Models;
using System;

namespace Freedom.UICore.Implement
{
    public class StatusBarService : IStatusBarService
    {
        public Action<string> DisplayUserName { get; set; }

        public Action<string> DisplayCompanyName { get; set; }

        public Action<string> DisplayShopName { get; set; }

        public Action<string> DisplayBoxName { get; set; }

        public Action<string> DisplayEmployee { get; set; }

        public Action<string> DisplayMoney { get; set; }

        public Action<StatusBarItem> DisplayAll { get; set; }
    }
}