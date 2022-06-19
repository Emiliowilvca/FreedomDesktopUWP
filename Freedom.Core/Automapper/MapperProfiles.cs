using AutoMapper;
using Freedom.Frontend.Models.Bindable;
using Freedom.Frontend.Models.BindableINFO;
using Freedom.Frontend.Models.BindableSqlite;
using Freedom.Frontend.Models.SqliteModels;
using Freedom.Utility;
using Freedom.Utility.Models.Dto;
using Freedom.Utility.Models.FullDto;
using Freedom.Utility.Models.RTO;

namespace Freedom.Core.Automapper
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {
            CreateMap<ApplyBind, ApplyDto>();
            CreateMap<ApplyDto, ApplyBind>();
            CreateMap<BankAccountBind, BankAccountDto>();
            CreateMap<BankAccountDto, BankAccountBind>();
            CreateMap<BankAccountTypeBind, BankAccountTypeDto>();
            CreateMap<BankAccountTypeDto, BankAccountTypeBind>();
            CreateMap<BankBind, BankDto>();
            CreateMap<BankDto, BankBind>();
            CreateMap<BoxBind, BoxDto>();
            CreateMap<BoxDto, BoxBind>();
            CreateMap<BranchBind, BranchDto>();
            CreateMap<BranchDto, BranchBind>();
            CreateMap<BrandBind, BrandDto>();
            CreateMap<BrandDto, BrandBind>();
            CreateMap<CarrierBind, CarrierDto>();
            CreateMap<CarrierDto, CarrierBind>();
            CreateMap<CategoryBind, CategoryDto>();
            CreateMap<CategoryDto, CategoryBind>();
            CreateMap<CityBind, CityDto>();
            CreateMap<CityDto, CityBind>();
            CreateMap<CompanyBind, CompanyDto>();
            CreateMap<CompanyDto, CompanyBind>();
            CreateMap<CountryBind, CountryDto>();
            CreateMap<CountryDto, CountryBind>();
            CreateMap<CustomerAccountBind, CustomerAccountDto>();
            CreateMap<CustomerAccountDto, CustomerAccountBind>();
            CreateMap<CustomerBind, CustomerDto>();
            CreateMap<CustomerDto, CustomerBind>();
            CreateMap<DrugBind, DrugDto>();
            CreateMap<DrugDto, DrugBind>();
            CreateMap<EmployeeBind, EmployeeDto>();
            CreateMap<EmployeeDto, EmployeeBind>();
            CreateMap<ExpenceBind, ExpenceDto>();
            CreateMap<ExpenceDto, ExpenceBind>();
            CreateMap<ExpenceTypeBind, ExpenceTypeDto>();
            CreateMap<ExpenceTypeDto, ExpenceTypeBind>();
            CreateMap<GroupBind, GroupDto>();
            CreateMap<GroupDto, GroupBind>();
            CreateMap<JobPostBind, JobPostDto>();
            CreateMap<JobPostDto, JobPostBind>();
            CreateMap<JobSectorBind, JobSectorDto>();
            CreateMap<JobSectorDto, JobSectorBind>();
            CreateMap<MeasureBind, MeasureDto>();
            CreateMap<MeasureDto, MeasureBind>();
            CreateMap<MoneyBind, MoneyDto>();
            CreateMap<MoneyDto, MoneyBind>();
            CreateMap<OperationTypeBind, OperationTypeDto>();
            CreateMap<OperationTypeDto, OperationTypeBind>();
            CreateMap<PackageBind, PackageDto>();
            CreateMap<PackageDto, PackageBind>();
            CreateMap<PaymentTypeBind, PaymentTypeDto>();
            CreateMap<PaymentTypeDto, PaymentTypeBind>();
            CreateMap<PriorityBind, PriorityDto>();
            CreateMap<PriorityDto, PriorityBind>();
            CreateMap<ProductBind, ProductDto>();
            CreateMap<ProductDto, ProductBind>();
            CreateMap<ProviderBind, ProviderDto>();
            CreateMap<ProviderDto, ProviderBind>();
            CreateMap<ProviderTypeBind, ProviderTypeDto>();
            CreateMap<ProviderTypeDto, ProviderTypeBind>();
            CreateMap<PurchaseBind, PurchaseDto>();
            CreateMap<PurchaseDto, PurchaseBind>();
            CreateMap<PurchaseFeesBind, PurchaseFeesDto>();
            CreateMap<PurchaseFeesDto, PurchaseFeesBind>();
            CreateMap<PurchaseWithHoldingTaxBind, PurchaseWithHoldingTaxDto>();
            CreateMap<PurchaseWithHoldingTaxDto, PurchaseWithHoldingTaxBind>();
            CreateMap<ReasonBind, ReasonDto>();
            CreateMap<ReasonDto, ReasonBind>();
            CreateMap<RouteBind, RouteDto>();
            CreateMap<RouteDto, RouteBind>();
            CreateMap<RoleBind, Role>();
            CreateMap<Role, RoleBind>();
            CreateMap<SectorProductBind, SectorProductDto>();
            CreateMap<SectorProductDto, SectorProductBind>();
            CreateMap<ShopBind, ShopDto>();
            CreateMap<ShopDto, ShopBind>();
            CreateMap<SourceProductDto, SourceProductBind>();
            CreateMap<SourceProductBind, SourceProductDto>();

            CreateMap<ShopRuleBind, ShopRuleDto>();
            CreateMap<ShopRuleDto, ShopRuleBind>();
            CreateMap<StateBind, StateDto>();
            CreateMap<StateDto, StateBind>();
            CreateMap<SubGroupBind, SubGroupDto>();
            CreateMap<SubGroupDto, SubGroupBind>();
            CreateMap<TaxBind, TaxDto>();
            CreateMap<TaxDto, TaxBind>();
            CreateMap<TownBind, TownDto>();
            CreateMap<TownDto, TownBind>();
            CreateMap<UserBind, UserDto>();
            CreateMap<UserDto, UserBind>();
            CreateMap<UserSettingBind, UserSettingDto>();
            CreateMap<UserSettingDto, UserSettingBind>();
            CreateMap<VehicleBind, VehicleDto>();
            CreateMap<VehicleDto, VehicleBind>();
            CreateMap<WayAdminBind, WayAdminDto>();
            CreateMap<WayAdminDto, WayAdminBind>();
            CreateMap<WithholdingTaxExportedBind, WithholdingTaxExportedDto>();
            CreateMap<WithholdingTaxExportedDto, WithholdingTaxExportedBind>();
            CreateMap<WithholdingVouchersBind, WithholdingVouchersDto>();
            CreateMap<WithholdingVouchersDto, WithholdingVouchersBind>();
            CreateMap<ZoneBind, ZoneDto>();
            CreateMap<ZoneDto, ZoneBind>();
        }
    }

    public class MapperProfilesSqlite : Profile
    {
        public MapperProfilesSqlite()
        {
            CreateMap<ApiUrlBaseBind, ApiUrlBase>();
            CreateMap<ApiUrlBase, ApiUrlBaseBind>();

            CreateMap<LoginHistory, LoginHistoryBind>();
            CreateMap<LoginHistoryBind, LoginHistory>();
        }
    }

    public class MapperProfilesRTOS : Profile
    {
        public MapperProfilesRTOS()
        {
            // CreateMap<ShopRuleRTO, ShopRuleINFO>();
            CreateMap<ApplyINFO, ApplyRTO>();
            CreateMap<ApplyRTO, ApplyINFO>();
            CreateMap<BankAccountINFO, BankAccountRTO>();
            CreateMap<BankAccountRTO, BankAccountINFO>();
            CreateMap<BankAccountTypeINFO, BankAccountTypeRTO>();
            CreateMap<BankAccountTypeRTO, BankAccountTypeINFO>();
            CreateMap<BankDepositINFO, BankDepositRTO>();
            CreateMap<BankDepositRTO, BankDepositINFO>();

            CreateMap<BankINFO, BankRTO>();
            CreateMap<BankRTO, BankINFO>();
            CreateMap<BoxRTO, BoxINFO>();
            CreateMap<BranchINFO, BranchRTO>();
            CreateMap<BranchRTO, BranchINFO>();
            CreateMap<BrandINFO, BrandRTO>();
            CreateMap<BrandRTO, BrandINFO>();
            CreateMap<CarrierRTO, CarrierINFO>();
            CreateMap<CategoryRTO, CategoryINFO>();
            CreateMap<CityRTO, CityINFO>();
            CreateMap<CompanyRTO, CompanyINFO>();
            CreateMap<CountryRTO, CountryINFO>();
            CreateMap<CustomerAccountINFO, CustomerAccountRTO>();
            CreateMap<CustomerAccountRTO, CustomerAccountINFO>();
            CreateMap<CustomerINFO, CustomerRTO>();
            CreateMap<CustomerRTO, CustomerINFO>();
            CreateMap<DrugRTO, DrugINFO>();
            CreateMap<EmployeeRTO, EmployeeINFO>();
            CreateMap<ExpenceTypeRTO, ExpenceTypeINFO>();
            CreateMap<ExpenseRTO, ExpenceINFO>();
            CreateMap<GroupRTO, GroupINFO>();
            CreateMap<JobPostRTO, JobPostINFO>();
            CreateMap<JobPostRTO, JobPostINFO>();
            CreateMap<JobSectorRTO, JobSectorINFO>();
            CreateMap<JobSectorRTO, JobSectorINFO>();
            CreateMap<MeasureRTO, MeasureINFO>();
            CreateMap<MoneyRTO, MoneyINFO>();
            CreateMap<OperationTypeRTO, OperationTypeINFO>();
            CreateMap<PackageRTO, PackageINFO>();
            CreateMap<PaymentTypeRTO, PaymentTypeINFO>();
            CreateMap<PaymentTypeRTO, PaymentTypeINFO>();
            CreateMap<PriorityRTO, PriorityINFO>();
            CreateMap<ProductRTO, ProductINFO>();
            CreateMap<ProviderRTO, ProviderINFO>();
            CreateMap<ProviderTypeRTO, ProviderTypeINFO>();
            CreateMap<PurchaseFeesRTO, PurchaseFeesINFO>();
            CreateMap<PurchaseRTO, PurchaseINFO>();
            CreateMap<ReasonRTO, ReasonINFO>();
            CreateMap<RouteRTO, RouteINFO>();
            CreateMap<SectorProductRTO, SectorProductINFO>();
            CreateMap<SourceProductRTO, SourceProductINFO>();
            CreateMap<ShopRTO, ShopINFO>();
            CreateMap<StateRTO, StateINFO>();
            CreateMap<SubClassOperationINFO, SubClassOperationRTO>();
            CreateMap<SubClassOperationRTO, SubClassOperationINFO>();
            CreateMap<SubGroupRTO, SubGroupINFO>();
            CreateMap<TaxRTO, TaxINFO>();
            CreateMap<TownRTO, TownINFO>();
            CreateMap<UserINFO, UserRTO>();
            CreateMap<UserRTO, UserINFO>();
            CreateMap<UserSettingBind, UserSettingRTO>();
            CreateMap<UserSettingRTO, UserSettingBind>();
            CreateMap<VehicleRTO, VehicleINFO>();
            CreateMap<WayAdminRTO, WayAdminINFO>();
            CreateMap<WithholdingVouchersRTO, WithholdingVouchersINFO>();
            CreateMap<ZoneRTO, ZoneINFO>();
        }
    }

    public class MapperProfilesDtoFull : Profile
    {
        public MapperProfilesDtoFull()
        {
            CreateMap<BankDtoFull, BankINFO>();
            CreateMap<BankAccountDtoFull, BankAccountINFO>();
        }
    }
}