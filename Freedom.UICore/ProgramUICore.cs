using AutoMapper;
using FluentValidation;
using Freedom.Core.ApiServiceFactory;
using Freedom.Core.ApiServiceImplementation;
using Freedom.Core.ApiServiceInterface;
using Freedom.Core.ApiServices;
using Freedom.Core.Automapper;
using Freedom.Core.CoreInterface;
using Freedom.Core.Implement;
using Freedom.Core.SQLiteConnectionFactory;
using Freedom.Core.SQLiteGenericRepository;
using Freedom.Core.SQLiteRepositoryImplement;
using Freedom.Core.SQLiteRepositoryInterface;
using Freedom.Frontend.Models.Bindable;
using Freedom.Frontend.RequestBind;
using Freedom.Frontend.ValidationBind;
//using Freedom.Report.FileImplement;
//using Freedom.Report.FileInterface;
//using Freedom.Report.ReportImplement;
//using Freedom.Report.ReportInterface;
using Freedom.UICore.Implement;
using Freedom.UICore.Interface;
using Freedom.UICore.ViewModels.BankViews;
using Freedom.UICore.ViewModels.MainViews;
using Freedom.UICore.ViewModels.SettingViews;
using Freedom.UICore.ViewModels.ShellViews;
//using Freedom.UICore.ViewModels.AccountViews;
//using Freedom.UICore.ViewModels.BankViews;
//using Freedom.UICore.ViewModels.CarrierViews;
//using Freedom.UICore.ViewModels.CashBoxViews;
//using Freedom.UICore.ViewModels.CompanyViews;
//using Freedom.UICore.ViewModels.CountryViews;
//using Freedom.UICore.ViewModels.CustomerViews;
//using Freedom.UICore.ViewModels.EmployeeViews;
//using Freedom.UICore.ViewModels.ExpenceViews;
//using Freedom.UICore.ViewModels.FeesViews;
//using Freedom.UICore.ViewModels.InventoryViews;
//using Freedom.UICore.ViewModels.MainViews;
//using Freedom.UICore.ViewModels.MoneyViews;
//using Freedom.UICore.ViewModels.OperationViews;
//using Freedom.UICore.ViewModels.PaymentViews;
//using Freedom.UICore.ViewModels.ProductViews;
//using Freedom.UICore.ViewModels.ProviderViews;
//using Freedom.UICore.ViewModels.PurchaseViews;
//using Freedom.UICore.ViewModels.ReportViews;
//using Freedom.UICore.ViewModels.ReturnViews;
//using Freedom.UICore.ViewModels.SaleViews;
//using Freedom.UICore.ViewModels.SearchViews;
//using Freedom.UICore.ViewModels.SettingViews;
//using Freedom.UICore.ViewModels.ShopViews;
//using Freedom.UICore.ViewModels.StatusBarViews;
//using Freedom.UICore.ViewModels.TransferViews;
//using Freedom.UICore.ViewModels.VehicleViews;
using Freedom.Utility.Cryptography.Implement;
using Freedom.Utility.Cryptography.Interface;
using Freedom.Utility.Implements;
using Freedom.Utility.Interfaces;
using Freedom.Utility.Models.BaseEntity;
using Freedom.Utility.Validation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//using NLog.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace Freedom.UICore
{
    public static class ProgramUICore
    {
        public static void RegisterInterfaces(this IServiceCollection services)
        {
            Assembly[] ViewsAssemblies = Thread.GetDomain().GetAssemblies()
                    .Where(x => x.GetName().Name == "Freedom.UICore" ||
                               x.GetName().Name == "Freedom.Desktop").ToArray();

            services.AddSingleton<INavigationService>(new NavigationService(ViewsAssemblies));
            services.AddSingleton<IReloadService, ReloadService>();
            services.AddSingleton<IShellNavigationService>(new ShellNavigationService(ViewsAssemblies));
            services.AddSingleton<IStatusNavigateService>(new StatusNavigateService(ViewsAssemblies));
            services.AddSingleton<IVariableService, VariableService>();
            services.AddScoped<IDeviceInfoService, DeviceInfoService>();
            services.AddScoped<IEncryption, Encryption>();
            services.AddScoped<IFindResourcesService, FindResourceService>();
            services.AddScoped<IFocusService, FocusService>();
            services.AddScoped<ILoadMoreHelper, LoadMoreHelper>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INetworkService, NetworkService>();
            services.AddScoped<IStatusBarService, StatusBarService>();
            services.AddScoped<IStatusNotifyService, StatusNotifyService>();
            services.AddScoped<IThemeSelectorService, ThemeSelectorService>();
            services.AddScoped<ITokenService, TokenService>();
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            //AppEssential.Configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)                
            // .AddJsonFile("appsettings.json", false)
            // .Build();
            // services.AddSingleton<IConfigurationRoot>(AppEssential.Configuration);
            //var ssssdd = AppEssential.Configuration.GetValue<string>("MyValues:value1") as string;
        }

        public static void RegisterSerilog(this IServiceCollection services)
        {
            //var serilogLogger = new LoggerConfiguration()
            //    .WriteTo.File("FreedomLogger.txt")
            //    .CreateLogger();
            //services.AddLogging(x =>
            //{
            //    x.SetMinimumLevel(LogLevel.Information);
            //    x.AddSerilog(logger: serilogLogger, dispose: true);
            //});

            services.AddLogging(log =>
            {
                log.ClearProviders();
                log.SetMinimumLevel(LogLevel.Trace);
              //  log.AddNLog("NLog.config");

                //log.AddNLog(new NLogProviderOptions
                //{
                //    CaptureMessageTemplates = true,
                //    CaptureMessageProperties = true
                //});
            });
        }

        public static void RegisterSQLiteRepositories(this IServiceCollection services)
        {
            string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            services.AddSingleton<IDbConnectionFactory>(new DbConnectionFactory(dbPath));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IApiUrlBaseRepository, ApiUrlBaseRepository>();
            services.AddScoped<IDarkThemeRepository, DarkThemeRepository>();
            services.AddScoped<ILoginHistoryRepository, LoginHistoryRepository>();
            services.AddScoped<ILoginStoreRepository, LoginStoreRepository>();
            services.AddScoped<ISqliteMigrationService, SqliteMigrationService>();
        }

        public static void RegisterAutomapper(this IServiceCollection services)
        {
            MapperConfiguration mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfiles());
                mc.AddProfile(new MapperProfilesRTOS());
                mc.AddProfile(new MapperProfilesSqlite());
                mc.AddProfile(new MapperProfilesDtoFull());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void RegisterApiServices(this IServiceCollection services)
        {
            services.AddTransient<IServiceFactory, ServiceFactory>();
            services.AddScoped(typeof(IApiService<,>), typeof(ApiService<,>));
            services.AddScoped<IApplyService, ApplyService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IBankAccountTypeService, BankAccountTypeService>();
            services.AddScoped<IBankService, BankService>();
            services.AddScoped<IBankDepositService, BankDepositService>();
            services.AddScoped<IBoxService, BoxService>();
            services.AddScoped<IBranchService, BranchService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ICarrierService, CarrierService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<ICustomerAccountService, CustomerAccountService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IDrugService, DrugService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IExpenceService, ExpenceService>();
            services.AddScoped<IExpenceTypeService, ExpenceTypeService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IJobPostService, JobPostService>();
            services.AddScoped<IJobSectorService, JobSectorService>();
            services.AddScoped<IMeasureService, MeaseureService>();
            services.AddScoped<IMoneyService, MoneyService>();
            services.AddScoped<IOperationSubClassService, OperationSubClassService>();
            services.AddScoped<IOperationTypeService, OperationTypeService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IPaymentTypeService, PaymentTypeService>();
            services.AddScoped<IPriorityService, PriorityService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProviderService, ProviderService>();
            services.AddScoped<IProviderTypeService, ProviderTypeService>();
            services.AddScoped<IPurchaseService, PurchaseSerice>();
            services.AddScoped<IReasonService, ReasonService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<ISectorProductService, SectorProductService>();
            services.AddScoped<IShopRuleService, ShopRuleService>();
            services.AddScoped<ISourceProductService, SourceProductService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ISubGroupService, SubGroupService>();
            services.AddScoped<ITaxService, TaxService>();
            services.AddScoped<ITimbradoService, TimbradoService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITownService, TownService>();
            services.AddScoped<IUserConfirmService, UserConfirmService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserSettingService, UserSettingService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IWayAdminService, WayAdminService>();
            services.AddScoped<IWithholdingVoucherService, WithholdingVoucherService>();
            services.AddScoped<IZoneService, ZoneService>();
        }

        public static void RegisterValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator<BankDepositBind>, BankDepositBindValidator>();
            services.AddScoped<IValidator<BankDepositDetailBind>, BankDepositDetailBindValidator>();
            services.AddScoped<IValidator<BankDepositRequestBind>, BankDepositRequestValidator>();

            services.AddScoped<IValidatePassword, ValidatePassword>();
            services.AddScoped<IValidator<IApply>, ApplyValidator>();
            services.AddScoped<IValidator<IBank>, BankValidator>();
            services.AddScoped<IValidator<IBankAccount>, BankAccountValidator>();
            services.AddScoped<IValidator<IBankAccountType>, BankAccountTypeValidator>();
            services.AddScoped<IValidator<IBankDeposit>, BankDepositValidator>();
            services.AddScoped<IValidator<IBankDepositDetail>, BankDepositDetailValidator>();
            services.AddScoped<IValidator<IBox>, BoxValidator>();
            services.AddScoped<IValidator<IBranch>, BranchValidator>();
            services.AddScoped<IValidator<IBrand>, BrandValidator>();
            services.AddScoped<IValidator<ICarrier>, CarrierValidator>();
            services.AddScoped<IValidator<ICategory>, CategoryValidator>();
            services.AddScoped<IValidator<ICity>, CityValidator>();
            services.AddScoped<IValidator<ICompany>, CompanyValidator>();
            services.AddScoped<IValidator<ICountry>, CountryValidator>();
            services.AddScoped<IValidator<ICustomer>, CustomerValidator>();
            services.AddScoped<IValidator<ICustomerAccount>, CustomerAccountValidator>();
            services.AddScoped<IValidator<IDrug>, DrugValidator>();
            services.AddScoped<IValidator<IEmployee>, EmployeeValidator>();
            services.AddScoped<IValidator<IExpenceType>, ExpenceTypeValidator>();
            services.AddScoped<IValidator<IGroup>, GroupValidator>();
            services.AddScoped<IValidator<IJobPost>, JobPostValidator>();
            services.AddScoped<IValidator<IJobSector>, JobSectorValidator>();
            services.AddScoped<IValidator<ILogin>, LoginValidator>();
            services.AddScoped<IValidator<IMeasure>, MeasureValidator>();
            services.AddScoped<IValidator<IMoney>, MoneyValidator>();
            services.AddScoped<IValidator<IOperationType>, OperationTypeValidator>();
            services.AddScoped<IValidator<IPackage>, PackageValidator>();
            services.AddScoped<IValidator<IPaymentType>, PaymentTypeValidator>();
            services.AddScoped<IValidator<IPaymentType>, PaymentTypeValidator>();
            services.AddScoped<IValidator<IPriority>, PriorityValidator>();
            services.AddScoped<IValidator<IProvider>, ProviderValidator>();
            services.AddScoped<IValidator<IProviderType>, ProviderTypeValidator>();
            services.AddScoped<IValidator<IPurchase>, PurchaseValidator>();
            services.AddScoped<IValidator<IPurchaseWithHoldingTax>, PurchaseWithHoldingTaxValidator>();
            services.AddScoped<IValidator<IReason>, ReasonValidator>();
            services.AddScoped<IValidator<IRoute>, RouteValidator>();
            services.AddScoped<IValidator<ISectorProduct>, SectorProductValidator>();
            services.AddScoped<IValidator<IShop>, ShopValidator>();
            services.AddScoped<IValidator<IShopRule>, ShopRuleValidator>();
            services.AddScoped<IValidator<ISourceProduct>, SourceProductValidator>();
            services.AddScoped<IValidator<IState>, StateValidator>();
            services.AddScoped<IValidator<ISubGroup>, SubGroupValidator>();
            services.AddScoped<IValidator<ITax>, TaxValidator>();
            services.AddScoped<IValidator<ITown>, TownValidator>();
            services.AddScoped<IValidator<IUser>, UserValidator>();
            services.AddScoped<IValidator<IUserSetting>, UserSettingValidator>();
            services.AddScoped<IValidator<IVehicle>, VehicleValidator>();
            services.AddScoped<IValidator<IWayAdmin>, WayAdminValidator>();
            services.AddScoped<IValidator<IZone>, ZoneValidator>();
        }

        public static void RegisterViewModels(this IServiceCollection services)
        {
            //services.AddTransient<ApplySearchViewModel>();
            //services.AddTransient<ApplyViewModel>();
            //services.AddTransient<BankAccountSearchViewModel>();
            services.AddTransient<BankAccountStatementViewModel>();
           // services.AddTransient<BankAccountTypeSearchViewModel>();
            services.AddTransient<BankAccountTypeViewModel>();
            services.AddTransient<BankAccountViewModel>();
            services.AddTransient<BankBouncedCheckViewModel>();
            services.AddTransient<BankCardsViewModel>();
          //  services.AddTransient<BankDepositReportViewModel>();
            services.AddTransient<BankDepositViewModel>();
            services.AddTransient<BankExtractionReportViewModel>();
            services.AddTransient<BankExtractionTypeViewModel>();
            services.AddTransient<BankExtractionViewModel>();
            services.AddTransient<BankReleaseCheckViewModel>();
          //  services.AddTransient<BankSearchViewModel>();
            services.AddTransient<BankTransfersViewModel>();
            services.AddTransient<BankViewModel>();
            //services.AddTransient<BoxSearchViewModel>();
            //services.AddTransient<BoxViewModel>();
            //services.AddTransient<BranchSearchViewModel>();
            //services.AddTransient<BranchViewModel>();
            //services.AddTransient<BrandSearchViewModel>();
            //services.AddTransient<BrandViewModel>();
            //services.AddTransient<BankDepositViewModel>();
            //services.AddTransient<CarrierSearchViewModel>();
            //services.AddTransient<CarrierViewModel>();
            //services.AddTransient<CashBoxCloseViewModel>();
            //services.AddTransient<CashBoxCountViewModel>();
            //services.AddTransient<CashBoxOpeningViewModel>();
            //services.AddTransient<CategorySearchViewModel>();
            //services.AddTransient<CategoryViewModel>();
            //services.AddTransient<CitySearchViewModel>();
            //services.AddTransient<CityViewModel>();
            //services.AddTransient<CompanySearchViewModel>();
            //services.AddTransient<CompanyViewModel>();
            services.AddTransient<ConfirmEmailViewModel>();
            //services.AddTransient<ConsumptionViewModel>();
            //services.AddTransient<CountrySearchViewModel>();
            //services.AddTransient<CountryViewModel>();
            //services.AddTransient<CustomerAccountSearchViewModel>();
            //services.AddTransient<CustomerAccountViewModel>();
            //services.AddTransient<CustomerFeesReportViewModel>();
            //services.AddTransient<CustomerFeesViewModel>();
            //services.AddTransient<CustomerPaymentReportViewModel>();
            //services.AddTransient<CustomerPaymentViewModel>();
            //services.AddTransient<CustomerSearchViewModel>();
            //services.AddTransient<CustomerViewModel>();
            //services.AddTransient<DrugSearchViewModel>();
            //services.AddTransient<DrugViewModel>();
            //services.AddTransient<EmployeeSearchViewModel>();
            //services.AddTransient<EmployeeViewModel>();
            //services.AddTransient<ExpenceTypeSearchViewModel>();
            //services.AddTransient<ExpenceTypeViewModel>();
            //services.AddTransient<ExpenceViewModel>();
            //services.AddTransient<FeesGeneratorViewModel>();
            //services.AddTransient<GroupSearchViewModel>();
            //services.AddTransient<GroupViewModel>();
            services.AddTransient<HomeViewModel>();
            //services.AddTransient<InventoryReportViewModel>();
            //services.AddTransient<InventoryViewModel>();
            //services.AddTransient<JobPostSearchViewModel>();
            //services.AddTransient<JobPostViewModel>();
            //services.AddTransient<JobSectorSearchViewModel>();
            //services.AddTransient<JobSectorViewModel>();
            //services.AddTransient<KardexReportViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainPageViewModel>();
            //services.AddTransient<MainWindowViewModel>();
            //services.AddTransient<MeasureSearchViewModel>();
            //services.AddTransient<MeasureViewModel>();
            //services.AddTransient<MoneyDenominationViewModel>();
            //services.AddTransient<MoneySearchViewModel>();
            //services.AddTransient<MoneyViewModel>();
            services.AddTransient<NewUserViewModel>();
            //services.AddTransient<OperationTypeSearchViewModel>();
            //services.AddTransient<OperationTypeViewModel>();
            //services.AddTransient<OrderNotDeliveredViewModel>();
            //services.AddTransient<PackageSearchViewModel>();
            //services.AddTransient<PackageViewModel>();
            //services.AddTransient<PaymentTypeSearchViewModel>();
            //services.AddTransient<PaymentTypeViewModel>();
            //services.AddTransient<PriceListViewModel>();
            //services.AddTransient<PriceTagViewModel>();
            //services.AddTransient<PrioritySearchViewModel>();
            //services.AddTransient<PriorityViewModel>();
            //services.AddTransient<ProductPriceViewModel>();
            //services.AddTransient<ProductViewModel>();
            //services.AddTransient<ProviderFeesReportViewModel>();
            //services.AddTransient<ProviderPaymentReportViewModel>();
            //services.AddTransient<ProviderPaymentViewModel>();
            //services.AddTransient<ProviderSearchViewModel>();
            //services.AddTransient<ProviderTypeSearchViewModel>();
            //services.AddTransient<ProviderTypeViewModel>();
            //services.AddTransient<ProviderViewModel>();
            //services.AddTransient<PurchaseOrderViewModel>();
            //services.AddTransient<PurchaseReportViewModel>();
            //services.AddTransient<PurchaseViewModel>();
            //services.AddTransient<PurchaseWithholdingTaxViewModel>();
            //services.AddTransient<ReasonReturnSearchViewModel>();
            //services.AddTransient<ReasonReturnViewModel>();
            services.AddTransient<RecoverPasswordViewModel>();
            //services.AddTransient<ReturnPurchaseViewModel>();
            //services.AddTransient<RouteSearchViewModel>();
            //services.AddTransient<RouteViewModel>();
            //services.AddTransient<SaleOrderViewModel>();
            //services.AddTransient<SaleReportViewModel>();
            //services.AddTransient<SaleReturnViewModel>();
            //services.AddTransient<SaleViewModel>();
            //services.AddTransient<SectorProductSearchViewModel>();
            //services.AddTransient<SectorProductViewModel>();
            services.AddTransient<SettingViewModel>();
            //services.AddTransient<ShopRuleViewModel>();
            //services.AddTransient<ShopViewModel>();
            //services.AddTransient<ShopSearchViewModel>();
            //services.AddTransient<SourceProductSearchViewModel>();
            //services.AddTransient<SourceProductViewModel>();
            //services.AddTransient<StateSearchViewModel>();
            //services.AddTransient<StateViewModel>();
            //services.AddTransient<StatusBarInfoViewModel>();
            //services.AddTransient<StatusBarNotifyViewModel>();
            //services.AddTransient<SubGroupSearchViewModel>();
            //services.AddTransient<SubGroupViewModel>();
            //services.AddTransient<TownSearchViewModel>();
            //services.AddTransient<TownViewModel>();
            //services.AddTransient<TransferProductViewModel>();
            //services.AddTransient<TransferReportViewModel>();
            services.AddTransient<UrlBaseViewModel>();
            //services.AddTransient<VehicleSearchViewModel>();
            //services.AddTransient<VehicleViewModel>();
            //services.AddTransient<WayAdminSearchViewModel>();
            //services.AddTransient<WayAdminViewModel>();
            //services.AddTransient<ZoneSearchViewModel>();
            //services.AddTransient<ZoneViewModel>();

            //services.AddTransient<UnoWebViewModel>();
        }

        public static void RegisterReports(this IServiceCollection services)
        {
            //services.AddScoped<IFileService, FileService>();

            //services.AddScoped<IBankDepositReport, BankDepositReport>();
        }
    }
}