using Freedom.Frontend.Models.Bindable;
using Freedom.Utility;
using Freedom.Utility.Models.Dto;
using System;
using System.Linq.Expressions;

namespace Freedom.Core
{
    public class ExpressionBankDeposit
    {
        public static Func<BankDepositBind, BankDepositDto> ConvertToBankDepositDto = x =>
        new BankDepositDto()
        {
            BankAccountId = x.BankAccountId,
            CompanyId = x.CompanyId,
            Concept = x.Concept.Trim().ToUpper(),
            DepositNumber = x.DepositNumber.ToLong(),
            Id = x.Id,
            OperationTypeId = x.OperationTypeId,
            Status = x.Status,
            TotalCash = x.TotalCash.ToDecimal(),
            TotalCheck = x.TotalCheck,
            TransactionDate = x.TransactionDate.ToDateTimeUtcNow(),
            MoneyId = x.MoneyId,
            UserId = x.UserId
        };

        public static readonly Expression<Func<BankDepositDetailBind, BankDepositDetailDto>> ToDepositDetailDto = (x) =>
        new BankDepositDetailDto
        {
            Amount = x.Amount.ToDecimal(),
            BankDepositId = x.BankDepositId,
            BankName = x.BankName.Trim().ToUpper(),
            CheckDate = x.CheckDate.ToDateTimeUtcNow(),
            CheckNumber = x.CheckNumber.ToLong(),
            ConceptCheck = x.Concept.Trim().ToUpper(),
            Id = x.Id,
            Sender = x.Sender.Trim().ToUpper()
        };

        public static Func<BankDepositDetailBind, BankDepositDetailDto> ToBankDepositDetailDto = x =>
        new BankDepositDetailDto
        {
            Amount = x.Amount.ToDecimal(),
            BankDepositId = x.BankDepositId,
            BankName = x.BankName.Trim().ToUpper(),
            CheckDate = x.CheckDate.ToDateTimeUtcNow(),
            CheckNumber = x.CheckNumber.ToLong(),
            ConceptCheck = x.Concept.Trim().ToUpper(),
            Id = x.Id,
            Sender = x.Sender.Trim().ToUpper()
        };
    }
}