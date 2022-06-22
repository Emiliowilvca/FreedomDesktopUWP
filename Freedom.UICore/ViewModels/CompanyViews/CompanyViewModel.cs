 
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
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.CompanyViews
{
    public class CompanyViewModel : BaseViewModel
    {
        private readonly string _controller = "/Companies";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly ICompanyService _companyService;
        private readonly IValidator<ICompany> _validator;

        public CompanyViewModel(ICompanyService companyService,
                                IValidator<ICompany> validator)
        {
            PageTitle = new PageTitle(Lang.Company, MaterialDesignIcons.OfficeBuildingCogOutline);
            _companyService = companyService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            Company = new CompanyBind();
        }

        #region Properties

        private CompanyBind _company;

        public CompanyBind Company { get => _company; set => SetProperty(ref _company, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CompanyINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CompanyINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<CompanyINFO>, string>(this, _sendMessageToken,
                async (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        await GetCompanyById(model.Id);
                       EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CompanyINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<CompanySearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
        }

        private async void SaveCommandExecute()
        {
            if (EnableControl)
                return;

            if (!_networkService.CheckIfInternet())
            {
                _statusNotifyService.ShowWarning(Lang.CheckInternetConnection);
                return;
            }

            var validationResult = ValidateCurrentEntity();
            if (!string.IsNullOrEmpty(validationResult))
            {
                _statusNotifyService.ShowWarning(validationResult);
                return;
            }
            CompanyDto companyDto = _mapper.Map<CompanyDto>(Company);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (companyDto.Id == 0)
                {
                    XResponse resp = await _companyService.PostAsync(_prefix, _version, _controller, _postMethod, companyDto,
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
                    XResponse resp = await _companyService.PutAsync(_prefix, _version, _controller, _putMethod, companyDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }
                Company.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(ICompany));
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
            EnableControl = true;
            Company.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Company);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        private async Task GetCompanyById(int companyId)
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
                var response = await _companyService.GetEntityAsync(_prefix, _version, _controller, "", companyId,
                                                                       _cancelTokenSource.Token, _variableService.Token);
                IsBusy = false;
                if (response.Entity == null) return;
                Company = ExpressionCompany.Instance().ConvertToCompanyBind(response.Entity);
            }
            catch (Exception ex)
            {
                IsBusy = false;
                _messageService.ShowMessage(ex.Message, "Load Company");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        #endregion Methods
    }
}