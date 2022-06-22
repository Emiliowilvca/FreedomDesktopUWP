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
using Freedom.UICore.Views.CustomerViews;
using Freedom.UICore.Views.SearchViews;
using Freedom.Utility;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.RTO;
using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Freedom.UICore.ViewModels.CustomerViews
{
    public class RouteViewModel : BaseViewModel
    {
        private readonly string _controller = "/Routes";
        private readonly string _zone = "/Zones";
        private readonly string _limit = "/limit";
        private readonly string _postMethod = "";
        private readonly string _putMethod = "";
        private readonly IRouteService _routeService;
        private readonly IZoneService _zoneService;
        private readonly IValidator<IRoute> _validator;

        public RouteViewModel(IRouteService routeService,
                              IZoneService zoneService,
                              IValidator<IRoute> validator)
        {
            PageTitle = new PageTitle(Lang.Route, MaterialDesignIcons.Highway);
            _routeService = routeService;
            _zoneService = zoneService;
            _validator = validator;
            _sendMessageToken = Guid.NewGuid().ToString();
            EnableControl = true;
            CreateCommand = new RelayCommand(CreateCommandExecute);
            EditCommand = new RelayCommand(EditCommandExecute);
            CancelCommand = new RelayCommand(CancelCommandExecute);
            SaveCommand = new RelayCommand(SaveCommandExecute);
            SearchZoneCommand = new RelayCommand(SearchZoneCommandExecute);
            Route = new RouteBind();
            ZoneCollection = new ObservableCollection<ZoneINFO>();
            _reloadService.Subscribe(nameof(RoutePage), typeof(IZone));
            LoadZones();
        }

        #region Properties

        private RouteBind _route;
        private ObservableCollection<ZoneINFO> _zoneCollection;

        public RouteBind Route { get => _route; set => SetProperty(ref _route, value); }

        public ObservableCollection<ZoneINFO> ZoneCollection { get => _zoneCollection; set => SetProperty(ref _zoneCollection, value); }

        #endregion Properties

        #region Commands

        public ICommand CreateCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand CancelCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }

        public ICommand SearchZoneCommand { get; private set; }

        private void CreateCommandExecute()
        {
            EnableControl = false;
            Route.CompanyId = _variableService.CompanyRTO.Id;
            _focusService.SetTextboxFocus("txtName");
        }

        private void EditCommandExecute()
        {
            bool resp = WeakReferenceMessenger.Default.IsRegistered<VMSendMessage<RouteINFO>, string>(this, _sendMessageToken);
            if (resp)
            {
                WeakReferenceMessenger.Default.Unregister<VMSendMessage<RouteINFO>, string>(this, _sendMessageToken);
            }
            WeakReferenceMessenger.Default.Register<VMSendMessage<RouteINFO>, string>(this, _sendMessageToken,
                (r, m) =>
                {
                    var model = m.Value.FirstOrDefault();
                    if (model != null)
                    {
                        Route.Name = model.Name;
                        Route.Id = model.Id;
                        Route.CompanyId = _variableService.CompanyRTO.Id;
                        Route.ZoneId = model.ZoneId;
                        EnableControl = false;
                        _focusService.SetTextboxFocus("txtName");
                    }
                    WeakReferenceMessenger.Default.Unregister<VMSendMessage<RouteINFO>, string>(this, _sendMessageToken);
                });
            _navigationService.NavigateTo<RouteSearchPage>(new NavigationParameter(1, _sendMessageToken.ToString()));
        }

        private void SearchZoneCommandExecute()
        {
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
            RouteDto routeDto = _mapper.Map<RouteDto>(Route);
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;

                if (routeDto.Id == 0)
                {
                    XResponse resp = await _routeService.PostAsync(_prefix, _version, _controller, _postMethod, routeDto,
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
                    XResponse resp = await _routeService.PutAsync(_prefix, _version, _controller, _putMethod, routeDto,
                                                    _cancelTokenSource.Token, _variableService.Token);
                    if (!resp.Success)
                    {
                        IsBusy = false;
                        _messageService.ShowMessage(resp.ResultMessage, "Error on Update");
                        return;
                    }
                }

                Route.ResetEntity();
                EnableControl = true;
                IsBusy = false;
                _statusNotifyService.ShowSuccess(Lang.RecordsProcessedSuccessfully);
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
            Route.ResetEntity();
        }

        #endregion Commands

        #region Methods

        private async void LoadZones()
        {
            RequestByLimit request = new RequestByLimit() { CompanyId = _variableService.CompanyRTO.Id, OffSet = 0, Limit = 999 };
            _cancelTokenSource = new CancellationTokenSource();
            _cancelTokenSource.CancelAfter(_findResourcesService.GetCancelTokenAfter());

            try
            {
                IsBusy = true;
                ListResponse<ZoneRTO> response = await _zoneService.GetListAsync(_prefix, _version, _zone, _limit,
                                                                   request, _cancelTokenSource.Token, _variableService.Token);

                if (!response.IsSuccess)
                {
                    IsBusy = false;
                    throw new Exception($"{response.Message}");
                }
                List<ZoneINFO> collection = _mapper.Map<List<ZoneINFO>>(response.Collection);

                ZoneCollection.Clear();

                ZoneCollection.AddRange(collection);

                IsBusy = false;
            }
            catch (Exception ex)
            {
                _messageService.ShowMessage(ex.Message, "Load Zone");
            }
            finally
            {
                _cancelTokenSource.Dispose();
            }
        }

        private string ValidateCurrentEntity()
        {
            ValidationResult result = _validator.Validate(Route);
            if (!result.IsValid)
            {
                ValidationFailure msg = result.Errors.FirstOrDefault();
                return msg.ErrorMessage;
            }
            return "";
        }

        public override void OnNavigateTo(object parameter)
        {
            base.OnNavigateTo(parameter);
            _reloadService.IsRecharge(nameof(RoutePage), typeof(IZone), LoadZones, true);
        }

        #endregion Methods
    }
}