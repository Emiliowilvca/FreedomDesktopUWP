using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using FluentValidation;
using FluentValidation.Results;
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
using Freedom.Utility.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.ProviderViews
{
    public class ProviderTypeViewModel : BaseViewModel
    {
        private readonly string _controller = "/ProviderTypes";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IProviderTypeService _providerTypeService;
        private readonly IValidator<IProviderType> _validator;

        public ProviderTypeViewModel(IProviderTypeService providerTypeService,
                                     IValidator<IProviderType> validator)
        {
            PageTitle = new PageTitle(Lang.ProviderType, MaterialDesignIcons.BriefcaseAccount);
            _providerTypeService = providerTypeService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            ProviderType = new ProviderTypeBind();
        }

        #region Properties

        private ProviderTypeBind _providerType;

        public ProviderTypeBind ProviderType { get => _providerType; set => SetProperty(ref _providerType, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            ProviderType.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtProviderType");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<ProviderTypeINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<ProviderTypeINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<ProviderTypeINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        ProviderType.Name = model.Name;
                        ProviderType.Id = model.Id;
                        ProviderType.CompanyId = _variableService.CompanyRTO.Id;
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<ProviderTypeINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<ProviderTypeSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
        }

        private void NavigationResult()
        {
            EnableControl = true;
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
            ProviderTypeDto providerTypeDto = _mapper.Map<ProviderTypeDto>(ProviderType);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (providerTypeDto.Id == 0)
                {
                    XResponse resp = await _providerTypeService.PostAsync(_prefix, _version, _controller, _postMethod, providerTypeDto,
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
                    XResponse resp = await _providerTypeService.PutAsync(_prefix, _version, _controller, _putMethod, providerTypeDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                ProviderType.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(IProviderType));
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
            ProviderType.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(ProviderType);
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