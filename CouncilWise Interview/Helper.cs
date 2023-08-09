using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouncilWise
{
    public static class Helper
    {
        internal const decimal TaxRate = 0.1m;
        internal const decimal TaxDivisor = 11;

        public static decimal CurrencyRound(this decimal value)
        {
            return Math.Round(value, 2);
        }
    }
}
