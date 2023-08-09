using System.Collections.Generic;
using System.Text;

namespace CouncilWise
{
    public class Receipt
    {
        public ICollection<ReceiptItem> Items { get; set; }
        public decimal Total { get; set; }
        public decimal TaxTotal { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Receipt:");
            sb.AppendLine("    Items:");
            foreach (var receiptItem in Items)
            {
                sb.AppendLine($"        {receiptItem.Name}");
                sb.AppendLine($"        {receiptItem.Quantity}\t\t{receiptItem.UnitPrice.CurrencyRound()}\t\t{receiptItem.TotalIncludesTax.CurrencyRound()}");
            }

            sb.AppendLine($"    Total:\t{Total.CurrencyRound()}");
            sb.AppendLine($"    GST: \t{TaxTotal.CurrencyRound()}");

            return sb.ToString();
        }
    }
}
