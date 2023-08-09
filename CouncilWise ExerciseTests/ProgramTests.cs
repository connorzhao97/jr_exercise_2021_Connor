using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CouncilWise.Tests
{
    [TestFixture]
    public class ProgramTests
    {
        [Test()]
        public void ProcessReceiptItemsTest()
        {
            var items = new List<ReceiptItem>();
            items.Add(new ReceiptItem { Name = "Bouncy Ball", Quantity = 4, UnitPrice = 1.15m, IncludesTax = true });
            items.Add(new ReceiptItem { Name = "Doll's House", Quantity = 1, UnitPrice = 213.99m, IncludesTax = true });
            items.Add(new ReceiptItem { Name = "In-store assist hrs", Quantity = 2, UnitPrice = 25.30m, IncludesTax = false });
            items.Add(new ReceiptItem { Name = "wAs It a Car Or a cat I saw", Quantity = 1, UnitPrice = 2.05m, IncludesTax = false });

            var receipt = Program.ProcessReceiptItems(items);
            Assert.Multiple(() =>
            {
                Assert.That(receipt.Items.Count, Is.EqualTo(4));

                Assert.That(receipt.Items.ElementAt(0).TaxAmount, Is.EqualTo(1.15m / 11));
                Assert.That(receipt.Items.ElementAt(0).TotalIncludesTax.CurrencyRound(), Is.EqualTo(4.60m));

                Assert.That(receipt.Items.ElementAt(2).TaxAmount, Is.EqualTo(2.53m));
                Assert.That(receipt.Items.ElementAt(2).TotalIncludesTax.CurrencyRound(), Is.EqualTo(55.66m));
                Assert.That(receipt.Items.ElementAt(2).IsPalindrome, Is.False);

                Assert.That(receipt.Items.ElementAt(3).IsPalindrome, Is.True);
                Assert.That(receipt.Items.ElementAt(3).TotalIncludesTax.CurrencyRound(), Is.EqualTo(0.00m));

                Assert.That(receipt.Total.CurrencyRound(), Is.EqualTo(274.25m));
                Assert.That(receipt.TaxTotal.CurrencyRound(), Is.EqualTo(24.93m));
            });
        }
    }
}