using System;
using System.Collections.Generic;
using System.Linq;

namespace CouncilWise
{
    /*	
   
    Requirements:

    - Given a list of receipt items where TaxAmount is not populated, calculate the correct tax for each receipt item. If IncludesGST is true, the unit price contains tax already. (In this case you will need to divide by 11 to get the tax amount). 
        If includesGST is false, the unit price does not contain tax and you will need to calculate it (multiply by 0.1 for tax amount). 
    - Return a populated Receipt object which contains the receipt items including tax amounts with the correct totals.
        Note that all totals shown on the receipt should be tax inclusive, including the receipt item totals.
    - For bonus points, if the receipt item name is a palindrome, the receipt item unit price should be changed to free. 
        A palindrome is a string which reads the same way forwards and backwards
    - You may modify the Receipt ReceiptItem and Helper classes any way you like as well as add new classes, but you must call the ProcessReceiptItems() method and the Receipt.ToString() method in your test case and no other methods.
    - 
    - Add additional test cases to cover any edge cases you can think of

    Expected Output:

    Print a receipt to the console with the following format: 

    Receipt:
	    Items:
	    <Name>
		    <Quantity>		<UnitPrice>		<Total inc. Tax>
	    <Name>			
		    <Quantity>		<UnitPrice>		<Total inc. Tax>
	    ... for all receipt items
    Total:	<Total inc. Tax>
    GST:	<TaxTotal>

    */
    public class Program
    {
        static void Main(string[] args)
        {
            RunTestCases();
        }

        /// <summary>
        /// Run through some possible scenarios for processing receipt items
        /// </summary>
        static void RunTestCases()
        {
            var items = new List<ReceiptItem>();
            items.Add(new ReceiptItem { Name = "Bouncy Ball", Quantity = 4, UnitPrice = 1.15m, IncludesTax = true });
            items.Add(new ReceiptItem { Name = "Doll's House", Quantity = 1, UnitPrice = 213.99m, IncludesTax = true });
            items.Add(new ReceiptItem { Name = "In-store assist hrs", Quantity = 2, UnitPrice = 25.30m, IncludesTax = false });
            var receiptResult = ProcessReceiptItems(items);
            Console.WriteLine(receiptResult.ToString());

            items = new List<ReceiptItem>();
            items.Add(new ReceiptItem { Name = "freebie eibeerf", Quantity = 4, UnitPrice = 1.15m, IncludesTax = true });
            receiptResult = ProcessReceiptItems(items);
            Console.WriteLine(receiptResult.ToString());

            // TODO: Add more test cases here to capture any edge cases you can think of 
            items = new List<ReceiptItem>();
            // Palindrome item not includes tax
            items.Add(new ReceiptItem { Name = "radar", Quantity = 3, UnitPrice = 12.15m, IncludesTax = false });
            // Palindrome item includes uppper case and lower case letters
            items.Add(new ReceiptItem { Name = "wAs It a Car Or a cat I saw", Quantity = 1, UnitPrice = 2.05m, IncludesTax = false });
            // Receipt item when unexpectedly entering a negative quantity
            items.Add(new ReceiptItem { Name = "Bouncy Ball", Quantity = -1, UnitPrice = 1.15m, IncludesTax = true });
            // Receipt item when unexpectedly entering a negative unit price
            items.Add(new ReceiptItem { Name = "Doll's House", Quantity = 1, UnitPrice = -1.15m, IncludesTax = false });
            // Receipt item when unexpectedly entering an empty name
            items.Add(new ReceiptItem { Name = "  ", Quantity = 1, UnitPrice = 1.15m, IncludesTax = true });
            receiptResult = ProcessReceiptItems(items);
            Console.WriteLine(receiptResult.ToString());

            // If the data source is fetched from the server, add error handlers
            // If the data is entered manually, add the validator
        }

        /// <summary>
        /// Process a list of receipt items to ensure correct tax is allocated
        /// </summary>
        /// <param name="items"></param>
        /// <returns>processed receipt</returns>
        public static Receipt ProcessReceiptItems(ICollection<ReceiptItem> items)
        {
            var receiptItems = items.Select(receiptItem =>
            {
                if (receiptItem.IsPalindrome)
                {
                    receiptItem.UnitPrice = 0.00m;
                }

                // Total invoice rule: 
                // Unrounded amounts of GST for each taxable sale should be totalled and then rounded to the nearest cent
                // It will be receiptItem.TaxAmount = receiptItem.UnitPrice / Helper.TaxDivisor;

                // Taxable supply rule
                // Work out the amount of GST for each individual taxable sale
                // It will be receiptItem.TaxAmount = (receiptItem.UnitPrice / Helper.TaxDivisor).CurrencyRound();

                if (receiptItem.IncludesTax)
                {
                    receiptItem.TaxAmount = receiptItem.UnitPrice / Helper.TaxDivisor;
                    receiptItem.TotalIncludesTax = receiptItem.UnitPrice * receiptItem.Quantity;
                }
                else
                {
                    receiptItem.TaxAmount = receiptItem.UnitPrice * Helper.TaxRate;
                    receiptItem.TotalIncludesTax = receiptItem.Quantity * (receiptItem.UnitPrice + receiptItem.TaxAmount);
                }

                return receiptItem;
            }).ToList();

            var receipt = new Receipt
            {
                Items = receiptItems,
                Total = receiptItems.Sum(r => r.TotalIncludesTax),
                TaxTotal = receiptItems.Sum(r => r.TaxAmount * r.Quantity)
            };

            return receipt;
        }
    }
}
