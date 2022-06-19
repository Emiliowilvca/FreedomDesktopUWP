using Freedom.Frontend.Models.AllPurpose;
using System;
using System.Collections.Generic;

namespace Freedom.Core.CoreInterface
{
    public interface IFeesCalculation
    {
        List<DeadlineType> DeadlineTypesGenerate();

        FinancingFees GenerateFees(FinancingFees f);

        FeesDetails CreateFeesDetail(int level, decimal capital, DateTime expiration,
                                     decimal interest, decimal lastBalance, string symbol);
    }
}