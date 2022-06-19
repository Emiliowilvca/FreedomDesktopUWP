using Freedom.Utility.Models.RTO;
using System;

namespace Freedom.Core.CoreInterface
{
    public interface IVariableService
    {
        UserRTO UserRTO { get; }

        ShopRTO ShopRTO { get; }

        ShopRuleRTO ShopRuleRTO { get; }

        BoxRTO BoxRTO { get; }

        UserSettingRTO UserSettingRTO { get; }

        CompanyRTO CompanyRTO { get; }

        CityRTO CityRTO { get; }

        EmployeeRTO EmployeeRTO { get; }

        MoneyRTO MoneyRTO { get; }

        string Token { get; }

        int LimitItem { get; }

        string InvoiceMask { get; }

        void InitializeVariables(Guid userId);

        void ReciveLimtItemValue(int limit);

        void ReceiveInvoiceMask(string invoiceMask);
    }
}