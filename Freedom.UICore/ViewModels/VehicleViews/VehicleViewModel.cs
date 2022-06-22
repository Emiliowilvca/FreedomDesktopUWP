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
using Freedom.Utility;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Responses;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.VehicleViews
{
    public class VehicleViewModel : BaseViewModel
    {
        private readonly string _controller = "/Vehicles";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IVehicleService _vehicleService;
        private readonly IValidator<IVehicle> _validator;
        private VehicleBind _vehicle;
        private string _capacity;
        private string _initialKm;
        private string _carriageNum;

        public VehicleViewModel(IVehicleService vehicleService,
                                IValidator<IVehicle> validator)
        {
            _vehicleService = vehicleService;
            _validator = validator;
            PageTitle = new PageTitle(Lang.Vehicle, MaterialDesignIcons.TruckCargoContainer);
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            Vehicle = new VehicleBind();
        }

        #region Properties

        public string Capacity { get => _capacity; set => SetProperty(ref _capacity, value); }

        public string InitialKm { get => _initialKm; set => SetProperty(ref _initialKm, value); }

        public string CarriageNum { get => _carriageNum; set => SetProperty(ref _carriageNum, value); }

        public VehicleBind Vehicle { get => _vehicle; set => SetProperty(ref _vehicle, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;

            Vehicle.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<VehicleINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<VehicleINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<VehicleINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Vehicle.Id = model.Id;
                        Vehicle.CompanyId = _variableService.CompanyRTO.Id;
                        Vehicle.ChassisNum = model.ChassisNum;
                        Vehicle.EngineNum = model.EngineNum;
                        Vehicle.CarriageNum = model.CarriageNum;
                        Vehicle.Brand = model.Brand;
                        Vehicle.Capacity = model.Capacity;
                        Vehicle.Color = model.Color;
                        Vehicle.FuelType = model.FuelType;
                        Vehicle.InitialKm = model.InitialKm;
                        Vehicle.LoadType = model.LoadType;
                        Vehicle.Patent = model.Patent;
                        Vehicle.TruckModel = model.TruckModel;
                        Vehicle.YearModel = model.YearModel;
                        Capacity = Vehicle.Capacity.ToString();
                        InitialKm = Vehicle.InitialKm.ToString();
                        CarriageNum = Vehicle.CarriageNum.ToString();
                        EnableControl = false;
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<VehicleINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<VehicleSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
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

            Vehicle.CarriageNum = CarriageNum.ToInteger();
            Vehicle.Capacity = Capacity.ToInteger();
            Vehicle.InitialKm = InitialKm.ToInteger();

            var validationResult = ValidateCurrentEntity();
            if (!string.IsNullOrEmpty(validationResult))
            {
                _statusNotifyService.ShowWarning(validationResult);
                return;
            }
            VehicleDto vehicleDto = _mapper.Map<VehicleDto>(Vehicle);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (vehicleDto.Id == 0)
                {
                    XResponse resp = await _vehicleService.PostAsync(_prefix, _version, _controller, _postMethod, vehicleDto,
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
                    XResponse resp = await _vehicleService.PutAsync(_prefix, _version, _controller, _putMethod, vehicleDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                Vehicle.ResetEntity();
                CarriageNum = "";
                InitialKm = "";
                Capacity = "";
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

        private void CancelCommandExecute()
        {
            EnableControl = true;
            Vehicle.ResetEntity();
            CarriageNum = "";
            InitialKm = "";
            Capacity = "";
        }

        #endregion Commands

        #region Methods

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Vehicle);
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