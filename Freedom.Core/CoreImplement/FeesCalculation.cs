using Freedom.Core.CoreInterface;
using Freedom.Frontend.Models.AllPurpose;
using Freedom.Utility.Langs;
using System;
using System.Collections.Generic;

namespace Freedom.Core.Implement
{
    public class FeesCalculation : IFeesCalculation
    {
        public List<DeadlineType> DeadlineTypesGenerate()
        {
            List<DeadlineType> deadlineTypes = new List<DeadlineType>();
            deadlineTypes.Add(new DeadlineType() { Id = 1, DayValue = 1, keyLang = nameof(Lang.EveryDay), Name = Lang.EveryDay });
            deadlineTypes.Add(new DeadlineType() { Id = 2, DayValue = 7, keyLang = nameof(Lang.Every8days), Name = Lang.Every8days });
            deadlineTypes.Add(new DeadlineType() { Id = 3, DayValue = 15, keyLang = nameof(Lang.Every15days), Name = Lang.Every15days });
            deadlineTypes.Add(new DeadlineType() { Id = 4, DayValue = 30, keyLang = nameof(Lang.Every30days), Name = Lang.Every30days });
            deadlineTypes.Add(new DeadlineType() { Id = 5, DayValue = 45, keyLang = nameof(Lang.Every45days), Name = Lang.Every45days });
            deadlineTypes.Add(new DeadlineType() { Id = 6, DayValue = 60, keyLang = nameof(Lang.Every60Days), Name = Lang.Every60Days });
            deadlineTypes.Add(new DeadlineType() { Id = 7, DayValue = 90, keyLang = nameof(Lang.Every90days), Name = Lang.Every90days });
            deadlineTypes.Add(new DeadlineType() { Id = 8, DayValue = 120, keyLang = nameof(Lang.Every120days), Name = Lang.Every120days });
            deadlineTypes.Add(new DeadlineType() { Id = 9, DayValue = 180, keyLang = nameof(Lang.Every180days), Name = Lang.Every180days });
            return deadlineTypes;
        }

        public FinancingFees GenerateFees(FinancingFees f)
        {
            FinancingFees financingFees = new FinancingFees(f.MoneyBind);

            decimal currentBalance = f.TotalAmount;
            decimal feesValue = f.BalanceToFinance / f.FeesQuantity;
            decimal interestFees = f.InterestTotal / f.FeesQuantity;
            DateTime lastDate = f.FirstMaturity;

            if (f.Delivery > 0)
            {
                var ss = f.TotalAmount + f.InterestTotal;
                FeesDetails fee = CreateFeesDetail(0, f.Delivery, DateTime.UtcNow, 0, ss, f.MoneyBind.Symbol);
                financingFees.FeesDetails.Add(fee);
            }

            currentBalance = f.BalanceToFinance + f.InterestTotal;

            for (int i = 0; i < f.FeesQuantity; i++)
            {
                DateTime expire = lastDate.AddDays(f.TermInDays);
                int level = i + 1;
                FeesDetails fee = CreateFeesDetail(level, feesValue, expire, interestFees,
                                                    currentBalance, f.MoneyBind.Symbol);
                financingFees.FeesDetails.Add(fee);
                currentBalance = fee.Balance;
                lastDate = fee.Expiration;
            }
            return financingFees;
        }

        public FeesDetails CreateFeesDetail(int level, decimal capital, DateTime expiration,
                                           decimal interest, decimal lastBalance, string symbol)
        {
            FeesDetails fees = new FeesDetails()
            {
                Capital = capital,
                CurrentDay = DateTime.UtcNow,
                Expiration = expiration,
                Interest = interest,
                LastBalance = lastBalance,
                Level = level,
                MoneySymbol = symbol
            };
            return fees;
        }
    }
}