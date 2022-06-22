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
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.CarrierViews
{
    public class CarrierViewModel : BaseViewModel
    {

        private readonly string _controller = "/Carriers";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IValidator<ICarrier> _validator;
        private readonly ICarrierService _carrierService;
        private CarrierBind _carrier;

        public CarrierViewModel(ICarrierService carrierService,
                                IValidator<ICarrier> validator)
        {
            Title = Lang.Carrier;
            PageTitle = new PageTitle(Lang.Carrier, MaterialDesignIcons.TruckFast);
            _carrierService = carrierService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            Carrier = new CarrierBind();
        }

        #region Properties
        public CarrierBind Carrier { get => _carrier; set => SetProperty(ref _carrier , value); }


        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            Carrier.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<CarrierINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<CarrierINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<CarrierINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Carrier.Name = model.Name;
                        Carrier.Id = model.Id;
                        Carrier.CompanyId = _variableService.CompanyRTO.Id;
                        Carrier.Address = model.Address;
                        Carrier.Phone = model.Phone;
                        Carrier.Mobile = model.Mobile;
                        Carrier.Person = model.Person;
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<CarrierINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<CarrierSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
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
            CarrierDto carrierDto = _mapper.Map<CarrierDto>(Carrier);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (carrierDto.Id == 0)
                {
                    XResponse resp = await _carrierService.PostAsync(_prefix, _version, _controller, _postMethod, carrierDto,
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
                    XResponse resp = await _carrierService.PutAsync(_prefix, _version, _controller, _putMethod, carrierDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                Carrier.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
                _reloadService.NotifyChanges(typeof(ICarrier));
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
            Carrier.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Carrier);
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