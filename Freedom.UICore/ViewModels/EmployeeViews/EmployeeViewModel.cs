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

namespace Freedom.UICore.ViewModels.EmployeeViews
{
    public class EmployeeViewModel : BaseViewModel
    {
        private readonly string _controller = "/Employees";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IEmployeeService _employeeService;
        private readonly IValidator<IEmployee> _validator;
        private readonly string _cityToken;
        private readonly string _jobSectorToken;
        private readonly string _jobPostToken;

        public EmployeeViewModel(IEmployeeService employeeService,
                                 IValidator<IEmployee> validator)
        {
            PageTitle = new PageTitle(Lang.Employee, MaterialDesignIcons.AccountHardHat);
            _employeeService = employeeService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            _cityToken = Guid.NewGuid().ToString();
            _jobPostToken = Guid.NewGuid().ToString();
            _jobSectorToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(Create);
            EditCommand = new RelayCommand(Edit);
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);
            SearchCityCommand = new RelayCommand(SearchCity);
            SearchJobPostCommand = new RelayCommand(SearchJobPost);
            SearchJobSectorCommand = new RelayCommand(SearchJobSector);
            Employee = new EmployeeBind();
        }

        #region Properties

        private EmployeeBind _employee;
        private string _cityName;
        private string _jobPostName;
        private string _jobSectorName;
        private string _salary;
        private string _commissionSales;
        private string _commissionCollection;
        private string _birthdate;
        private string _workStarDate;
        private string _childrens;

        public EmployeeBind Employee { get => _employee; set => SetProperty(ref _employee, value); }

        public string CityName { get => _cityName; set => SetProperty(ref _cityName, value); }

        public string JobPostName { get => _jobPostName; set => SetProperty(ref _jobPostName, value); }

        public string JobSectorName { get => _jobSectorName; set => SetProperty(ref _jobSectorName, value); }

        public string Salary { get => _salary; set => SetProperty(ref _salary, value); }

        public string CommissionSales { get => _commissionSales; set => SetProperty(ref _commissionSales, value); }

        public string CommissionCollection { get => _commissionCollection; set => SetProperty(ref _commissionCollection, value); }

        public string Birthdate { get => _birthdate; set => SetProperty(ref _birthdate, value); }

        public string WorkStarDate { get => _workStarDate; set => SetProperty(ref _workStarDate, value); }

        public string Childrens { get => _childrens; set => SetProperty(ref _childrens, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchCityCommand { get; private set; }

        public ICommand SearchJobPostCommand { get; private set; }

        public ICommand SearchJobSectorCommand { get; private set; }

        private void Create()
        {
            EnableControl = false;
            Employee.ActiveWorked = true;
            Salary = "0";
            CommissionCollection = "0";
            CommissionSales = "0";
            Childrens = "0";
            Birthdate = DateTime.UtcNow.AddYears(-20).ToShortDateString();
            WorkStarDate = DateTime.UtcNow.ToShortDateString();

            Employee.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void Edit()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<EmployeeINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<EmployeeINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<EmployeeINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        GetEmployeeById(model.Id);
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<EmployeeINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<EmployeeSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
        }

        private async void Save()
        {
            if (EnableControl)
                return;

            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }
            if (!ValidateLocal())
                return;

            Employee.Salary = Salary.ToDecimal();
            Employee.CommissionSales = CommissionSales.ToDecimal();
            Employee.CommissionCollection = CommissionCollection.ToDecimal();
            Employee.BirtDate = Convert.ToDateTime(Birthdate);
            Employee.WorkStarDate = Convert.ToDateTime(WorkStarDate);
            Employee.ChildCount = Childrens.ToInteger();

            var validationResult = ValidateCurrentEntity();
            if (!string.IsNullOrEmpty(validationResult))
            {
                _statusNotifyService.ShowWarning(validationResult);
                return;
            }
            EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(Employee);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (employeeDto.Id == 0)
                {
                    XResponse resp = await _employeeService.PostAsync(_prefix, _version, _controller, _postMethod, employeeDto,
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
                    XResponse resp = await _employeeService.PutAsync(_prefix, _version, _controller, _putMethod, employeeDto,
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
                _reloadService.NotifyChanges(typeof(IEmployee));
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
            CityName = "";
            JobPostName = "";
            JobSectorName = "";
            Salary = "";
            Birthdate = "";
            Childrens = "";
            CommissionSales = "";
            CommissionCollection = "";
            WorkStarDate = "";
            Employee.ResetEntity();
            EnableControl = true;
        }

        #endregion Commands

        #region Methods

        private async void GetEmployeeById(int employeeId)
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
                var response = await _employeeService.GetEntityFullAsync<EmployeeDtoFull>(_prefix, _version, _controller, "", employeeId,
                                                                                      _cancelTokenSource.Token, _variableService.Token);
                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }
                IsBusy = false;
                var employee = response.Entity;
                if (employee == null) return;
                Employee = ExpressionEmployee.Instance().ConvertToEmployeeBind(employee);

                CityName = employee.CityName;
                JobPostName = employee.JobPostName;
                JobSectorName = employee.JobSectorName;
                Childrens = employee.ChildCount.ToString();
                Salary = employee.Salary.ToString("N2", new CultureInfo("en-US"));
                CommissionCollection = employee.CommissionCollection.ToString("N2", new CultureInfo("en-US"));
                CommissionSales = employee.CommissionSales.ToString("N2", new CultureInfo("en-US"));
                Birthdate = employee.BirtDate.ToShortDateString();
                WorkStarDate = employee.WorkStarDate.ToShortDateString();
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

        private bool ValidateLocal()
        {
            if (!Salary.IsDecimals())
            {
                _messageService.ShowMessage($"{Lang.SalaryIsNotValid}", "Error");
                return false;
            }
            if (!CommissionSales.IsDecimals())
            {
                _messageService.ShowMessage($"{Lang.CommissionPercentIsNotValid}", "Error");
                return false;
            }

            if (!CommissionCollection.IsDecimals())
            {
                _messageService.ShowMessage($"{Lang.CommissionCollectionIsNotValid}", "Error");
                return false;
            }
            if (!Birthdate.IsValidDate())
            {
                _messageService.ShowMessage(Lang.BirthDateIsInvalid, Lang.Birthdate);
                return false;
            }

            if (!WorkStarDate.IsValidDate())
            {
                _messageService.ShowMessage(Lang.WorkStartDateIsInvalid, Lang.WorkStartDate);
                return false;
            }
            return true;
        }

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Employee);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        private void SearchJobSector()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<JobSectorINFO>, string>(this, _jobSectorToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<JobSectorINFO>, string>(this, _jobSectorToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<JobSectorINFO>, string>(this, _jobSectorToken,
                (r, m) =>
                {
                    JobSectorINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Employee.JobSectorId = model.Id;
                        JobSectorName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<JobSectorINFO>, string>(this, _jobSectorToken);
                });
            _navigationService.NavigateTo<JobSectorSearchPage>(new NavigationParameter(1, _jobSectorToken.ToString()));
        }

        private void SearchJobPost()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<JobPostINFO>, string>(this, _jobPostToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<JobPostINFO>, string>(this, _jobPostToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<JobPostINFO>, string>(this, _jobPostToken,
                (r, m) =>
                {
                    JobPostINFO model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Employee.JobPostId = model.Id;
                        JobPostName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<JobPostINFO>, string>(this, _jobPostToken);
                });
            _navigationService.NavigateTo<JobPostSearchPage>(new NavigationParameter(1, _jobPostToken.ToString()));
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
                        Employee.CityId = model.Id;
                        CityName = model.Name;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CityINFO>, string>(this, _cityToken);
                });
            _navigationService.NavigateTo<CitySearchPage>(new NavigationParameter(1, _cityToken.ToString()));
        }

        #endregion Methods
    }
}