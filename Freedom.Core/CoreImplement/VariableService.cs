using Freedom.Core.CoreInterface;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Utility.Models.RTO;
using System;
using System.Linq;
using System.Text.Json;

namespace Freedom.Core.Implement
{
    public class VariableService : IVariableService
    {
        private readonly ILoginStoreRepository _loginStoreRepository;

        public VariableService(ILoginStoreRepository loginStoreRepository)
        {
            _loginStoreRepository = loginStoreRepository;

            UserRTO = new UserRTO();
            ShopRTO = new ShopRTO();
            ShopRuleRTO = new ShopRuleRTO();
            BoxRTO = new BoxRTO();
            UserSettingRTO = new UserSettingRTO();
            CompanyRTO = new CompanyRTO();
            CityRTO = new CityRTO();
            Token = "";
        }

        #region PublicProperties

        private UserRTO _userRTO;
        private string _token;
        private ShopRTO _shopRTO;
        private ShopRuleRTO _shopRuleRTO;
        private BoxRTO _boxRTO;
        private UserSettingRTO _userSettingRTO;
        private CompanyRTO _companyRTO;
        private CityRTO _cityRTO;
        private EmployeeRTO _employeeRTO;
        private MoneyRTO _moneyRTO;
        private int _limitItem;
        private string _invoiceMask;

        public UserRTO UserRTO { get => _userRTO; private set => _userRTO = value; }

        public ShopRTO ShopRTO { get => _shopRTO; private set => _shopRTO = value; }

        public ShopRuleRTO ShopRuleRTO { get => _shopRuleRTO; private set => _shopRuleRTO = value; }

        public BoxRTO BoxRTO { get => _boxRTO; private set => _boxRTO = value; }

        public UserSettingRTO UserSettingRTO { get => _userSettingRTO; private set => _userSettingRTO = value; }

        public CompanyRTO CompanyRTO { get => _companyRTO; private set => _companyRTO = value; }

        public CityRTO CityRTO { get => _cityRTO; private set => _cityRTO = value; }

        public EmployeeRTO EmployeeRTO { get => _employeeRTO; private set => _employeeRTO = value; }

        public MoneyRTO MoneyRTO { get => _moneyRTO; private set => _moneyRTO = value; }

        public string Token { get => _token; private set => _token = value; }

        public int LimitItem { get => _limitItem; private set => _limitItem = value; }

        public string InvoiceMask { get => _invoiceMask; private set => _invoiceMask = value; }

        #endregion PublicProperties

        #region Methods

        public void InitializeVariables(Guid userId)
        {
            var loginStore = _loginStoreRepository.GetAll(x => x.UserId == userId).FirstOrDefault();
            if (loginStore != null)
            {
                _token = loginStore.Token;
                _userRTO = JsonSerializer.Deserialize<UserRTO>(loginStore.User);
                _shopRTO = JsonSerializer.Deserialize<ShopRTO>(loginStore.Shop);
                _shopRuleRTO = JsonSerializer.Deserialize<ShopRuleRTO>(loginStore.ShopRule);
                _boxRTO = JsonSerializer.Deserialize<BoxRTO>(loginStore.Box);
                _userSettingRTO = JsonSerializer.Deserialize<UserSettingRTO>(loginStore.UserSetting);
                _companyRTO = JsonSerializer.Deserialize<CompanyRTO>(loginStore.Company);
                _cityRTO = JsonSerializer.Deserialize<CityRTO>(loginStore.City);
                _employeeRTO = JsonSerializer.Deserialize<EmployeeRTO>(loginStore.Employee);
                _moneyRTO = JsonSerializer.Deserialize<MoneyRTO>(loginStore.Money);
            }
        }

        public void ReciveLimtItemValue(int limit)
        {
            _limitItem = limit;
        }

        public void ReceiveInvoiceMask(string mask)
        {
            _invoiceMask = mask;
        }

        #endregion Methods
    }
}