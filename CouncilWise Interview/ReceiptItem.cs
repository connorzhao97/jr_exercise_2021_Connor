using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CouncilWise
{
    public class ReceiptItem
    {
        public string Name { get; set; }
        public bool IsPalindrome
        {
            get
            {
                var name = Name.Replace(" ", string.Empty);
                var lowerCasedName = name.ToLower();
                var length = name.Length;

                // Method 1:
                for (int i = 0; i < length / 2; i++)
                {
                    if (lowerCasedName[i] != lowerCasedName[length - 1 - i])
                    {
                        return false;
                    }
                }
                return true;

                // Method 2:
                // var lowerCasedNameReversed = lowerCasedName.Reverse();
                // return lowerCasedName.SequenceEqual(lowerCasedNameReversed);
            }
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set
            {
                if (value < 0)
                {
                    _quantity = 0;
                }
                else
                {
                    _quantity = value;
                }
            }
        }

        private decimal _unitPrice;
        public decimal UnitPrice
        {
            get => _unitPrice;
            set
            {
                if (value < 0)
                {
                    _unitPrice = 0.00m;
                }
                else
                {
                    _unitPrice = value;
                }
            }
        }
        public bool IncludesTax { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalIncludesTax { get; set; }
    }
}
