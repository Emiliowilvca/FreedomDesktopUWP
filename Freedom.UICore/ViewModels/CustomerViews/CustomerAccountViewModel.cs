using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using FluentValidation.Results;
using Freedom.Core;
using Freedom.Core.ApiServiceInterface;
 
using Freedom.Frontend.FontIcons;
using Freedom.Frontend.Models.Bindable;
using Freedom.Frontend.Models.BindableINFO;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Models;
using Freedom.UICore.SendingMessages;
using Freedom.UICore.Views.SearchViews;
using Freedom.Utility;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Responses;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.CustomerViews
{
    public class CustomerAccountViewModel : BaseViewModel
    {
        private readonly string _controller = "/CustomerAccount";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IValidator<ICustomerAccount> _validator;
        private readonly ICustomerAccountService _customerAccountService;
        private readonly string _customerToken;
        private readonly string _shopToken;
        private readonly string _moneyToken;
        private readonly string _employeeToken;

        public CustomerAccountViewModel(ICustomerAccountService customerAccountService,
                                        IValidator<ICustomerAccount> validator)
        {
            PageTitle = new PageTitle(Lang.CustomerAccount, MaterialDesignIcons.BadgeAccountHorizontalOutline);
            _customerAccountService = customerAccountService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            _customerToken = Guid.NewGuid().ToString();
            _moneyToken = Guid.NewGuid().ToString();
            _shopToken = Guid.NewGuid().ToString();
            _customerToken = Guid.NewGuid().ToString();
            _employeeToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(Create);
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
            SearchCustomerCommand = new RelayCommand(SearchCustomer);
            SearchEmployeeCommand = new RelayCommand(SearchEmployee);
            SearchShopCommand = new RelayCommand(SearchShop);
            SearchMoneyCommand = new RelayCommand(SearchMoney);

            CustomerAccount = new CustomerAccountBind();
        }

        #region Properties

        private CustomerAccountBind _customerAccount;
        private string _customerName;
        private string _moneyName;
        private string _shopName;
        private string _employeeName;
        private string _startDate;
        private string _endDate;
        private string _creditLimit;
        private string _accountNum;

        public CustomerAccountBind CustomerAccount { get => _customerAccount; set => SetProperty(ref _customerAccount, value); }

        public string AccountNum { get => _accountNum; set => SetProperty(ref _accountNum, value); }

        public string CustomerName { get => _customerName; set => SetProperty(ref _customerName, value); }

        public string MoneyName { get => _moneyName; set => SetProperty(ref _moneyName, value); }

        public string ShopName { get => _shopName; set => SetProperty(ref _shopName, value); }

        public string EmployeeName { get => _employeeName; set => SetProperty(ref _employeeName, value); }

        public string StartDate { get => _startDate; set => SetProperty(ref _startDate, value); }

        public string EndDate { get => _endDate; set => SetProperty(ref _endDate, value); }

        public string CreditLimit { get => _creditLimit; set => SetProperty(ref _creditLimit, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchCustomerCommand { get; private set; }

        public ICommand SearchEmployeeCommand { get; private set; }

        public ICommand SearchMoneyCommand { get; private set; }

        public ICommand SearchShopCommand { get; private set; }

        private void Create()
        {
            EnableControl = false;
            AccountNum = "0";
            StartDate = DateTime.UtcNow.ToShortDateString();
            EndDate = DateTime.UtcNow.AddYears(10).ToShortDateString();
            CustomerAccount.Active = true;
            CustomerAccount.CompanyId = _variableService.CompanyRTO.Id;
            CustomerAccount.ShopId = _variableService.ShopRTO.Id;
            CustomerAccount.MoneyId = _variableService.MoneyRTO.Id;
            CustomerAccount.CompanyId = _variableService.CompanyRTO.Id;
            CustomerAccount.StartDate = DateTime.UtcNow;
            ShopName = _variableService.ShopRTO.Name;
            CustomerAccount.Name = Lang.CustomerAccount;
            MoneyName = _variableService.MoneyRTO.Name;
            _focusService.SetTextboxFocus("txtName");
        }

        private void Edit()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CustomerAccountINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CustomerAccountINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<CustomerAccountINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        GetCustomerAccountById(model.Id);
                        //Apply.Name = model.Name;
                        //Apply.Id = model.Id;
                        //Apply.CompanyId = _variableService.CompanyRTO.Id;
                        //EnableControl = false;
                        //_focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CustomerAccountINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<CustomerAccountSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
        }

        private async void Save()
        {
            if (EnableControl)
                return;

            if (!CreditLimit.IsDecimals())
            {
                _statusNotifyService.ShowWarning(Lang.CreditLimitIsInvalid);
                return;
            }
            if (!EndDate.IsValidDate())
            {
                _statusNotifyService.ShowWarning(Lang.DateUntilIsNotValid);
                return;
            }

            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }

            CustomerAccount.CreditLimit = CreditLimit.ToDecimal();
            CustomerAccount.EndDate = Convert.ToDateTime(EndDate);
            CustomerAccount.Name = CustomerAccount.Name.ToUpper();
            var validationResult = ValidateCurrentEntity();
            if (!string.IsNullOrEmpty(validationResult))
            {
                _statusNotifyService.ShowWarning(validationResult);
                return;
            }
            CustomerAccountDto customerAccountDto = _mapper.Map<CustomerAccountDto>(CustomerAccount);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (customerAccountDto.Id == 0)
                {
                    XResponse resp = await _customerAccountService.PostAsync(_prefix, _version, _controller, _postMethod, customerAccountDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Insert");
                        return;
                    }
                }
                else
                {
                    XResponse resp = await _customerAccountService.PutAsync(_prefix, _version, _controller, _putMethod, customerAccountDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                CustomerAccount.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(IApply));
            }
            catch (TaskCanceledException Tcex)
            {
                IsBusy = false;
                _statusNotifyService.ShowError(Tcex.Message);
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private void Cancel()
        {
            AccountNum = "";
            CustomerName = "";
            MoneyName = "";
            ShopName = "";
            StartDate = "";
            EmployeeName = "";
            StartDate = "";
            EndDate = "";
            CreditLimit = "";
            EnableControl = true;
            CustomerAccount.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private void SearchCustomer()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CustomerINFO>, string>(this, _customerToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CustomerINFO>, string>(this, _customerToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<CustomerINFO>, string>(this, _customerToken,
                (r, m) =>
                {
                    CustomerINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        CustomerAccount.CustomerId = model.Id;
                        CustomerName = model.FullName;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CustomerINFO>, string>(this, _customerToken);
                });
            _navigationService.NavigateTo<CustomerSearchPage>(new NavigationParameter(1, _customerToken.ToString()));
        }

        private void SearchShop()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<ShopINFO>, string>(this, _shopToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<ShopINFO>, string>(this, _shopToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<ShopINFO>, string>(this, _shopToken,
                (r, m) =>
                {
                    ShopINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        CustomerAccount.ShopId = model.Id;
                        ShopName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<ShopINFO>, string>(this, _shopToken);
                });
            _navigationService.NavigateTo<ShopSearchPage>(new NavigationParameter(1, _shopToken.ToString()));
        }

        private void SearchMoney()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<MoneyINFO>, string>(this, _moneyToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<MoneyINFO>, string>(this, _moneyToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<MoneyINFO>, string>(this, _moneyToken,
                (r, m) =>
                {
                    MoneyINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        CustomerAccount.MoneyId = model.Id;
                        MoneyName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<MoneyINFO>, string>(this, _moneyToken);
                });
            _navigationService.NavigateTo<MoneySearchPage>(new NavigationParameter(1, _moneyToken.ToString()));
        }

        private void SearchEmployee()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<EmployeeINFO>, string>(this, _employeeToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<EmployeeINFO>, string>(this, _employeeToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<EmployeeINFO>, string>(this, _employeeToken,
                (r, m) =>
                {
                    EmployeeINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        CustomerAccount.EmployeeId = model.Id;
                        EmployeeName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<EmployeeINFO>, string>(this, _employeeToken);
                });
            _navigationService.NavigateTo<EmployeeSearchPage>(new NavigationParameter(1, _employeeToken.ToString()));
        }

        private async void GetCustomerAccountById(int accountId)
        {
            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }

            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());
            try
            {
                IsBusy = true;
                var response = await _customerAccountService.GetEntityFullAsync<CustomerAccountDtoFull>(_prefix, _version, _controller, "", accountId,
                                                                                      _cancelTokenSource.Token, _variableService.Token);
                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }
                IsBusy = false;
                var cus = response.Entity;
                if (cus == null) return;

                CustomerAccount = ExpressionCustomerAccount.ConvertToCustomerAccountBind(cus);

                AccountNum = cus.AccountNum.ToString("0000000");
                CustomerName = cus.CustomerName;
                MoneyName = cus.MoneyName;
                ShopName = cus.ShopName;
                StartDate = cus.StartDate.ToShortDateString();
                EndDate = cus.EndDate.ToShortDateString();
                EmployeeName = cus.EmployeeName;
                CreditLimit = cus.CreditLimit.ToString("N2", new CultureInfo("en-US"));
                EnableControl = false;
                _focusService.SetTextboxFocus("txtName");
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _messageService.ShowMessage(ex.Message, "Load Privider");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(CustomerAccount);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        #endregion Methods
    }
}