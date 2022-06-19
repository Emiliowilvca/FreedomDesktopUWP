using Freedom.Frontend.Models.Bindable;
using Freedom.Utility.Models.FullDto;
using System;

namespace Freedom.Core
{
    public class ExpressionCustomerAccount
    {
        public static Func<CustomerAccountDtoFull, CustomerAccountBind> ConvertToCustomerAccountBind = (x) => new CustomerAccountBind()
        {
            Id = x.Id,
            Name = x.Name,
            AccountNum = x.AccountNum,
            Active = x.Active,
            CompanyId = x.CompanyId,
            CreditLimit = x.CreditLimit,
            CustomerId = x.CustomerId,
            EmployeeId = x.EmployeeId,
            EndDate = x.EndDate,
            MoneyId = x.MoneyId,
            ShopId = x.ShopId,
            StartDate = x.StartDate
        };
    }
}