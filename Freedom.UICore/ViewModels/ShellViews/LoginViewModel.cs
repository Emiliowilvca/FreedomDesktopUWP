using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.FrontendVariable;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Frontend.Models.Bindable;
using Freedom.Frontend.Models.BindableSqlite;
using Freedom.Frontend.Models.SqliteModels;
using Freedom.UICore.BaseClass;
using Freedom.UICore.Interface;
using Freedom.UICore.Views.ShellViews;
using Freedom.Utility.Cryptography.Interface;
using Freedom.Utility.Langs;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Request;
using Freedom.Utility.Responses;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using FluentValidation.Results;
using System.Linq;
using Freedom.Utility;
using System.Text.Json;
using System.Diagnostics;

namespace Freedom.UICore.ViewModels.ShellViews
{
    public class LoginViewModel : BaseViewModel
    {
        /*===========================================================================================
                                                LOGIN VIEW-MODEL
        ===========================================================================================*/

        private readonly string _accountcontroller = "/Account";
        private readonly string _postCreateToken = "/CreateToken";
        private readonly IValidator<ILogin> _validatorLogin;
        private readonly ITokenService _tokenService;
        private readonly IEncryption _encryption;
        private readonly ISqliteMigrationService _sqliteMigrationService;
        private readonly ILoginStoreRepository _loginStoreRepository;
        private readonly IApiUrlBaseRepository _apiUrlBaseRepository;
        private readonly ILoginHistoryRepository _loginHistoryRepository;
        private readonly ILogger<LoginViewModel> _logger;
        private readonly IShellNavigationService _shellNavigationService;

        public LoginViewModel(IShellNavigationService shellNavigationService,
                              IValidator<ILogin> validatorLogin,
                              ITokenService tokenService,
                              IEncryption encryption,
                              ISqliteMigrationService sqliteMigrationService,
                              ILoginStoreRepository loginStoreRepository,
                              IApiUrlBaseRepository apiUrlBaseRepository,
                              ILoginHistoryRepository loginHistoryRepository,
                              ILogger<LoginViewModel> logger)
        {
            _validatorLogin = validatorLogin;
            _tokenService = tokenService;
            _encryption = encryption;
            _sqliteMigrationService = sqliteMigrationService;
            _loginStoreRepository = loginStoreRepository;
            _apiUrlBaseRepository = apiUrlBaseRepository;
            _loginHistoryRepository = loginHistoryRepository;
            _logger = logger;
            _shellNavigationService = shellNavigationService;
            UrlBaseCommand = new RelayCommand(UrlBaseCommandExecute);
            LoginCommand = new RelayCommand(LoginCommandExecute);
            NewUserCommand = new RelayCommand(NewUserCommandExecute);
            RecoverPasswordCommand = new RelayCommand(RecoverPasswordCommandExecute);
            LoginBind = new LoginBind();
            UserEmailCollection = new ObservableCollection<string>();
            _sqliteMigrationService.CreateAndMigrateAllTables();
            IsBusy = false;
        }

        #region Properties

        private ObservableCollection<string> _userEmailCollection;
        private LoginBind _loginBind;

        public LoginBind LoginBind
        {
            get => _loginBind;
            set => SetProperty(ref _loginBind, value);
        }

        public ObservableCollection<string> UserEmailCollection
        {
            get => _userEmailCollection;
            set => SetProperty(ref _userEmailCollection, value);
        }

        #endregion Properties

        #region Commands

        public ICommand LoginCommand { get; private set; }

        public ICommand UrlBaseCommand { get; private set; }

        public ICommand NewUserCommand { get; private set; }

        public ICommand RecoverPasswordCommand { get; private set; }

        public ICommand OnNavigateCommand { get; private set; }

        private void NewUserCommandExecute()
        {
            _shellNavigationService.Navigate<NewUserPage>();
        }

        private async void LoginCommandExecute()
        {
            try
            {
                _logger.LogInformation("LoginViewModel {BussinesLayerEvent} at {Datetime}", "Started", DateTime.Now);

                ValidationResult result = _validatorLogin.Validate(LoginBind);

                if (!result.IsValid)
                {
                    string errormsg = result.Errors?.FirstOrDefault()?.ErrorMessage ?? "Error on Validation";
                    _messageService.ShowMessage(errormsg, "WRONG PARAMETER");
                    return;
                }

                if (!_networkService.CheckIfInternet())
                {
                    _messageService.ShowMessage(Lang.CheckInternetConnection, "Connection Error");
                    return;
                }
                CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();

                int cancetime = _findResourcesService.GetCancelTokenAfter();
                _cancelTokenSource.CancelAfter(cancetime);

                await LoginAsync(_cancelTokenSource);
                _logger.LogError("La concha de la lora", "nomelase");
            }
            catch (Exception ex)
            {
                IsBusy = false;
                Debug.WriteLine(ex.Message);
                _messageService.ShowMessage(ex.Message, "LoginCommandExecute");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void UrlBaseCommandExecute()
        {
            _shellNavigationService.Navigate<UrlBasePage>();
        }

        private void RecoverPasswordCommandExecute()
        {
            _shellNavigationService.Navigate<RecoverPasswordPage>();
        }

        public override void OnNavigateTo(object parameter)
        {
            base.OnNavigateTo(parameter);
            LoadUrlBase();
            ResolveRemember();
        }

        #endregion Commands

        #region Methods

        private void ResolveRemember()
        {
            List<LoginStore> loginstoreCollection = _loginStoreRepository.GetAll();
            if (loginstoreCollection != null)
            {
                LoginStore loginstore = loginstoreCollection.FirstOrDefault();
                if (loginstore != null && loginstore.RememberMe)
                {
                    LoginBind.Email = loginstore.Email;
                    LoginBind.Password = _encryption.DecryptString(loginstore.Password);
                    LoginBind.Rememberme = loginstore.RememberMe;
                }
            }
        }

        private void SearchEmailSuggets(string email)
        {
            UserEmailCollection.Clear();
            List<string> emails = _loginHistoryRepository.GetAll().Select(x => x.Email).Distinct()
                                      .Where(x => x.Contains(email)).ToList();
            UserEmailCollection.AddRange(emails);
        }

        private void LoadUrlBase()
        {
            ApiUrlBase apiUrlBase = _apiUrlBaseRepository.GetAll().FirstOrDefault();
            ApiUrlBaseBind apiUrlBaseBind = _mapper.Map<ApiUrlBaseBind>(apiUrlBase);
            if (apiUrlBase != null)
            {
                LoginBind.EndPoint = apiUrlBase.EndPoint;
                AppGlobalVar.ApiUrlBaseBind = apiUrlBaseBind;
            }
        }

        public void AutoSuggestBoxEmail_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                SearchEmailSuggets(sender.Text);
            }
        }

        private async Task LoginAsync(CancellationTokenSource cancelTokenSource)
        {
            try
            {
                TokenRequest tokenRequest = new TokenRequest()
                {
                    Email = LoginBind.Email,
                    Password = LoginBind.Password
                };
                IsBusy = true;
                TokenResponse result = await _tokenService.GetTokenAsync(_prefix, _version, _accountcontroller, _postCreateToken, tokenRequest, cancelTokenSource.Token);

                IsBusy = false;

                //confirmar email
                if (!result.Success && result.ResourceKey == nameof(Lang.InvalidLoginAttempt))
                {
                    _shellNavigationService.Navigate<ConfirmEmailPage>();
                    return;
                }

                if (!result.Success)
                {
                    _messageService.ShowMessage(result.ResultMessage, "GetTokenAsync");
                    return;
                }

                SaveLoginStore(result, LoginBind);

                SaveUserHistory(LoginBind);

                InitializeVariables(result.UserRTO.Id);

                _shellNavigationService.Navigate<MainPage>();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void SaveLoginStore(TokenResponse result, LoginBind login)
        {
            LoginStore loginStore = _loginStoreRepository.GetFirstOrDefault();
            if (loginStore == null)
            {
                LoginStore newloginStore = CreateLoginStore(Guid.NewGuid(), result, login);
                _loginStoreRepository.Insert(newloginStore);
            }
            else
            {
                LoginStore updateloginStore = CreateLoginStore(loginStore.Id, result, login);
                _loginStoreRepository.Update(updateloginStore);
            }
        }

        private LoginStore CreateLoginStore(Guid id, TokenResponse result, LoginBind login)
        {
            LoginStore loginStore = new LoginStore()
            {
                Id = id,
                UserId = result.UserRTO.Id,
                ExpireToken = result.ExpireToken,
                Success = result.Success,
                Token = result.Token,
                Email = result.UserRTO.Email,
                Password = _encryption.EncryptString(login.Password),
                RememberMe = login.Rememberme,
                User = JsonSerializer.Serialize(result.UserRTO),
                Company = JsonSerializer.Serialize(result.CompanyRTO),
                Box = JsonSerializer.Serialize(result.BoxRTO),
                City = JsonSerializer.Serialize(result.CityRTO),
                Employee = JsonSerializer.Serialize(result.EmployeeRTO),
                Money = JsonSerializer.Serialize(result.MoneyRTO),
                Shop = JsonSerializer.Serialize(result.ShopRTO),
                ShopRule = JsonSerializer.Serialize(result.ShopRuleRTO),
                UserSetting = JsonSerializer.Serialize(result.UserSettingRTO)
            };
            return loginStore;
        }

        private void SaveUserHistory(LoginBind login)
        {
            LoginHistoryBind history = new LoginHistoryBind()
            {
                Id = Guid.NewGuid(),
                LastDateAccess = DateTime.Now,
                EndPoint = login.EndPoint,
                Email = login.Email,
                Password = _encryption.EncryptString(login.Password),
                Rememberme = login.Rememberme
            };
            LoginHistory loginhistory = _mapper.Map<LoginHistory>(history);
            _loginHistoryRepository.Insert(loginhistory);
        }

        private void InitializeVariables(Guid id)
        {
            _variableService.InitializeVariables(id);
            int limitOnGetItem = _findResourcesService.GetStruct<int>("SearchView.LimitOnGetItem");
            string invoicemask = _findResourcesService.GetStruct<string>("Invoice.Mask");
            _variableService.ReciveLimtItemValue(limitOnGetItem);
            _variableService.ReceiveInvoiceMask(invoicemask);
        }

        #endregion Methods
    }
}