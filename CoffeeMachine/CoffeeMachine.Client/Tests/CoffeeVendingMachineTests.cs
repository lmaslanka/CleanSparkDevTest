namespace CoffeeMachine.Client.Tests
{
    using Xunit;

    public class CoffeeVendingMachineTests
    {
        [Fact]
        public void AddCoffeeOrder_ValidCoffeeOrder_SingleOrderAdded()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Small, 0, 0);

            Assert.Single(coffeeMachine.GetCoffeeOrders());
        }

        [Fact]
        public void AddCoffeeOrder_ValidMultipleCoffeeOrders_MultipleOrdersAdded()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Small, 0, 0);
            coffeeMachine.AddCoffeeToOder(CoffeeSize.Medium, 0, 0);
            coffeeMachine.AddCoffeeToOder(CoffeeSize.Large, 0, 0);
            coffeeMachine.AddCoffeeToOder(CoffeeSize.Large, 2, 2);

            Assert.Equal(4, coffeeMachine.GetCoffeeOrders().Count);
        }

        [Fact]
        public void AddCoffeeOrder_InvalidCoffeeSize_NoOrderAdded()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder((CoffeeSize)4, 0, 0);

            Assert.Empty(coffeeMachine.GetCoffeeOrders());
        }

        [Fact]
        public void AddCoffeeOrder_NegativeSugarNumber_NoOrderAdded()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Large, InvalidNegativeNumberOfAddOns, 0);

            Assert.Empty(coffeeMachine.GetCoffeeOrders());
        }

        [Fact]
        public void AddCoffeeOrder_LargeSugarNumber_NoOrderAdded()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Large, InvalidPositiveNumberOfAddOns, 0);

            Assert.Empty(coffeeMachine.GetCoffeeOrders());
        }

        [Fact]
        public void AddCoffeeOrder_NegativeCreamNumber_NoOrderAdded()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Large, 0, InvalidNegativeNumberOfAddOns);

            Assert.Empty(coffeeMachine.GetCoffeeOrders());
        }

        [Fact]
        public void AddCoffeeOrder_LargeCreamNumber_NoOrderAdded()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Large, 0, InvalidPositiveNumberOfAddOns);

            Assert.Empty(coffeeMachine.GetCoffeeOrders());
        }

        [Fact]
        public void AddCoffeeOrder_AddSingleSmallCoffeeNoAddOns_OrderTotalCorect()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Small, 0, 0);

            Assert.Equal(CoffeeOrder.SmallCoffeePrice, coffeeMachine.GetOrderTotal());
        }

        [Fact]
        public void AddCoffeeOrder_AddSingleMediumCoffeeNoAddOns_OrderTotalCorect()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Medium, 0, 0);

            Assert.Equal(CoffeeOrder.MediumCoffeePrice, coffeeMachine.GetOrderTotal());
        }

        [Fact]
        public void AddCoffeeOrder_AddSingleLargeCoffeeNoAddOns_OrderTotalCorect()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Large, 0, 0);

            Assert.Equal(CoffeeOrder.LargeCoffeePrice, coffeeMachine.GetOrderTotal());
        }

        [Fact]
        public void AddCoffeeOrder_AddSingleMediumCoffeeOneCreamOneSugar_OrderTotalCorect()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Medium, 1, 1);

            Assert.Equal(CoffeeOrder.MediumCoffeePrice + CoffeeOrder.SugarPrice + CoffeeOrder.CreamerPrice, coffeeMachine.GetOrderTotal());
        }

        [Fact]
        public void AddPaymentToOrder_ValidPaymentAmount_CorrectAmountIsAdded()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddPayment(ValidPaymentAmount);

            Assert.Equal(ValidPaymentAmount, coffeeMachine.GetPaymentTotal());
        }

        [Fact]
        public void AddPaymentToOrder_InvalidPaymentAmount_NoAmountIsAdded()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddPayment(InvalidPaymentAmount);

            Assert.Equal(0.0D, coffeeMachine.GetPaymentTotal());
        }

        [Fact]
        public void AddPaymentToOrder_MultipleValidAmounts_TotalIsValid()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);

            Assert.Equal(0.35D, coffeeMachine.GetPaymentTotal());
        }

        [Fact]
        public void AddPaymentToOrder_MultipleValidAndInvalidAmounts_TotalIsValid()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(InvalidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);
            coffeeMachine.AddPayment(ValidPaymentAmount);

            Assert.Equal(0.30D, coffeeMachine.GetPaymentTotal());
        }

        [Fact]
        public void Checkout_GreaterAmountGiven_ValidChangeExpected()
        {
            var coffeeMachine = new CoffeeVendingMachine();

            coffeeMachine.AddCoffeeToOder(CoffeeSize.Small, 0, 0);
            coffeeMachine.AddCoffeeToOder(CoffeeSize.Medium, 0, 0);
            coffeeMachine.AddCoffeeToOder(CoffeeSize.Large, 0, 0);
            coffeeMachine.AddCoffeeToOder(CoffeeSize.Large, 2, 2);

            coffeeMachine.AddPayment(ValidLargePaymentAmount);

            Assert.Equal(ExpectedChangeAmount, coffeeMachine.CalculateChange());
        }

        private const int InvalidNegativeNumberOfAddOns = -1;
        private const int InvalidPositiveNumberOfAddOns = 4;

        private const double InvalidPaymentAmount = 0.03D;
        private const double ValidPaymentAmount = 0.05D;
        private const double ValidLargePaymentAmount = 20.0D;
        private const double ExpectedChangeAmount = 10.25D;
    }
}
