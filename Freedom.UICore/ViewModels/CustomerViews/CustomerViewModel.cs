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
using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.CustomerViews
{
    public class CustomerViewModel : BaseViewModel
    {
        private readonly string _controller = "/Customers";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IValidator<ICustomer> _validator;
        private readonly ICustomerService _customerService;
        private readonly string _cityToken;
        private readonly string _categoryToken;
        private readonly string _branchToken;
        private readonly string _routeToken;

        public CustomerViewModel(ICustomerService customerService,
                                 IValidator<ICustomer> validator)
        {
            PageTitle = new PageTitle(Lang.Customer, MaterialDesignIcons.AccountMultiple);
            _customerService = customerService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            _cityToken = Guid.NewGuid().ToString();
            _categoryToken = Guid.NewGuid().ToString();
            _branchToken = Guid.NewGuid().ToString();
            _routeToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            SearchCityCommand = new RelayCommand(SearchCity);
            SearchCategoryCommand = new RelayCommand(SearchCategory);
            SearchBranchCommand = new RelayCommand(SearchBranch);
            SearchRouteCommand = new RelayCommand(SearchRoute);
            Customer = new CustomerBind();
            IniatilizeGenders();
        }

        #region Properties

        private CustomerBind _customer;
        private string _cityName;
        private string _categoryName;
        private string _branchName;
        private string _routeName;
        private string _partnerCI;
        private ObservableCollection<SexualGender> _sexualGenderCollection;

        private string _dateResidence;
        private string _workStartDate;
        private string _birthDate;
        private string _childrens;
        private string _maxDiscountPercent;

        public CustomerBind Customer { get => _customer; set => SetProperty(ref _customer, value); }

        public ObservableCollection<SexualGender> SexualGenderCollection
        {
            get => _sexualGenderCollection;
            set => SetProperty(ref _sexualGenderCollection, value);
        }

        public string CityName { get => _cityName; set => SetProperty(ref _cityName, value); }

        public string CategoryName { get => _categoryName; set => SetProperty(ref _categoryName, value); }

        public string BranchName { get => _branchName; set => SetProperty(ref _branchName, value); }

        public string RouteName { get => _routeName; set => SetProperty(ref _routeName, value); }

        public string PartnerCI { get => _partnerCI; set => SetProperty(ref _partnerCI, value); }

        public string Childrens { get => _childrens; set => SetProperty(ref _childrens, value); }

        public string MaxDiscountPercent { get => _maxDiscountPercent; set => SetProperty(ref _maxDiscountPercent, value); }

        public string DateResidence { get => _dateResidence; set => SetProperty(ref _dateResidence, value); }

        public string WorkStartDate { get => _workStartDate; set => SetProperty(ref _workStartDate, value); }

        public string BirthDate { get => _birthDate; set => SetProperty(ref _birthDate, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchCityCommand { get; set; }

        public ICommand SearchCategoryCommand { get; set; }

        public ICommand SearchBranchCommand { get; set; }

        public ICommand SearchRouteCommand { get; set; }

        private void CreateCommandExecute()
        {
            DateResidence = DateTime.UtcNow.ToShortDateString();
            WorkStartDate = DateTime.UtcNow.ToShortDateString();
            BirthDate = DateTime.UtcNow.AddYears(-25).ToShortDateString();
            Customer.PriceLevel = 1;
            Customer.Credit = true;
            MaxDiscountPercent = "0";
            Childrens = "0";
            EnableControl = false;
            Customer.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CustomerINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CustomerINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<CustomerINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        GetCustomerById(model.Id);
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CustomerINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<CustomerSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
        }

        private async void SaveCommandExecute()
        {
            if (EnableControl)
                return;

            if (!ValidateLocal())
                return;

            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }

            Customer.PartnerCI = PartnerCI.ToInteger();// _numericHelper.ExtractIntNumbersFromString(PartnerCI);
            Customer.Children = (byte)PartnerCI.ToInteger();
            Customer.MaxDiscount = MaxDiscountPercent.ToDecimal();
            Customer.BirthDate = Convert.ToDateTime(BirthDate);
            Customer.EmployementDate = Convert.ToDateTime(WorkStartDate);
            Customer.ResidenceDate = Convert.ToDateTime(DateResidence);
            Customer.DateAdmin = DateTime.Now;

            var validationResult = ValidateCurrentEntity();
            if (!string.IsNullOrEmpty(validationResult))
            {
                _statusNotifyService.ShowWarning(validationResult);
                return;
            }
            CustomerDto customerDto = _mapper.Map<CustomerDto>(Customer);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (customerDto.Id == 0)
                {
                    XResponse resp = await _customerService.PostAsync(_prefix, _version, _controller, _postMethod, customerDto,
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
                    XResponse resp = await _customerService.PutAsync(_prefix, _version, _controller, _putMethod, customerDto,
                                                                     _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }
                IsBusy = false;
                Cancel();
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

        private void CancelCommandExecute()
        {
            Cancel();
        }

        private void SearchRoute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<RouteINFO>, string>(this, _routeToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<RouteINFO>, string>(this, _routeToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<RouteINFO>, string>(this, _routeToken,
                (r, m) =>
                {
                    RouteINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Customer.RouteId = model.Id;
                        RouteName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<RouteINFO>, string>(this, _routeToken);
                });
            _navigationService.NavigateTo<RouteSearchPage>(new NavigationParameter(1, _routeToken.ToString()));
        }

        private void SearchBranch()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<BranchINFO>, string>(this, _branchToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<BranchINFO>, string>(this, _branchToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<BranchINFO>, string>(this, _branchToken,
                (r, m) =>
                {
                    BranchINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Customer.BranchId = model.Id;
                        BranchName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<BranchINFO>, string>(this, _branchToken);
                });
            _navigationService.NavigateTo<BranchSearchPage>(new NavigationParameter(1, _branchToken.ToString()));
        }

        private void SearchCategory()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CategoryINFO>, string>(this, _categoryToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CategoryINFO>, string>(this, _categoryToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<CategoryINFO>, string>(this, _categoryToken,
                (r, m) =>
                {
                    CategoryINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Customer.CategoryId = model.Id;
                        CategoryName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CategoryINFO>, string>(this, _categoryToken);
                });
            _navigationService.NavigateTo<CategorySearchPage>(new NavigationParameter(1, _categoryToken.ToString()));
        }

        private void SearchCity()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CityINFO>, string>(this, _cityToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _cityToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<CityINFO>, string>(this, _cityToken,
                (r, m) =>
                {
                    CityINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Customer.CityId = model.Id;
                        CityName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _cityToken);
                });
            _navigationService.NavigateTo<CitySearchPage>(new NavigationParameter(1, _cityToken.ToString()));
        }

        #endregion Commands

        #region Methods

        private async void GetCustomerById(int customerId)
        {
            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }
            RequestById request = new RequestById() { CompanyId = _variableService.CompanyRTO.Id, Id = customerId };
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());
            try
            {
                IsBusy = true;
                var response = await _customerService.GetEntityFullAsync<CustomerDtoFull>(_prefix, _version, _controller, "", customerId,
                                                                                      _cancelTokenSource.Token, _variableService.Token);
                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }
                IsBusy = false;
                var cus = response.Entity;
                if (cus == null) return;
                Customer = ExpressionCustomer.Instance().ConvertToCompanyBind(cus);

                CityName = cus.CityName;
                CategoryName = cus.CategoryName;
                BranchName = cus.BranchName;
                RouteName = cus.RouteName;
                PartnerCI = cus.PartnerCI.ToString();
                Childrens = cus.Children.ToString();
                MaxDiscountPercent = cus.MaxDiscount.ToString("N2", new CultureInfo("en-US"));
                DateResidence = cus.ResidenceDate.ToShortDateString();
                WorkStartDate = cus.EmployementDate.ToShortDateString();
                BirthDate = cus.BirthDate.ToShortDateString();
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
            ValidationResult result = _validator.Validate(Customer);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        private void IniatilizeGenders()
        {
            SexualGenderCollection = new ObservableCollection<SexualGender>();
            SexualGenderCollection.Add(new SexualGender() { Id = 0, GenderName = Lang.Indeterminate });
            SexualGenderCollection.Add(new SexualGender() { Id = 1, GenderName = Lang.male });
            SexualGenderCollection.Add(new SexualGender() { Id = 2, GenderName = Lang.Female });
        }

        private void Cancel()
        {
            EnableControl = true;
            Customer.ResetEntity();
            CityName = "";
            CategoryName = "";
            BranchName = "";
            RouteName = "";
            PartnerCI = "";
            DateResidence = "";
            WorkStartDate = "";
            BirthDate = "";
            Childrens = "";
            MaxDiscountPercent = "";
        }

        private bool ValidateLocal()
        {
            if (!BirthDate.IsValidDate())
            {
                _messageService.ShowMessage(Lang.BirthDateIsInvalid, Lang.Birthdate);
                return false;
            }

            if (!DateResidence.IsValidDate())
            {
                _messageService.ShowMessage($" the {Lang.ResidenceDate} is invalid", Lang.ResidenceDate);
                return false;
            }

            if (!WorkStartDate.IsValidDate())
            {
                _messageService.ShowMessage($" the {Lang.WorkStartDate} is invalid", Lang.WorkStartDate);
                return false;
            }
            if (!MaxDiscountPercent.IsDecimals())
            {
                _messageService.ShowMessage($" the {Lang.MaxDiscountPercent} is invalid", "Converter");
                return false;
            }
            return true;
        }

        #endregion Methods
    }
}