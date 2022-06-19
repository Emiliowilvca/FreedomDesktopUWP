using CommunityToolkit.Mvvm.ComponentModel;

namespace Freedom.UICore.Models
{
    public class StatusBarItem : ObservableObject
    {
        private string _userEmail;
        private string _shopName;
        private string _boxName;
        private string _companyName;
        private string _moneyName;
        private string _employeeName;

        public StatusBarItem()
        {
        }

        public StatusBarItem(string userEmail, string companyName, string shopName, string boxName, string employeName, string moneyName)
        {
            UserEmail = userEmail;
            ShopName = shopName;
            BoxName = boxName;
            CompanyName = companyName;
            EmployeeName = employeName;
            MoneyName = moneyName;
        }

        public string UserEmail { get => _userEmail; set => SetProperty(ref _userEmail, value); }

        public string ShopName { get => _shopName; set => SetProperty(ref _shopName, value); }

        public string BoxName { get => _boxName; set => SetProperty(ref _boxName, value); }

        public string CompanyName { get => _companyName; set => SetProperty(ref _companyName, value); }

        public string MoneyName { get => _moneyName; set => SetProperty(ref _moneyName, value); }

        public string EmployeeName { get => _employeeName; set => SetProperty(ref _employeeName, value); }
    }
}