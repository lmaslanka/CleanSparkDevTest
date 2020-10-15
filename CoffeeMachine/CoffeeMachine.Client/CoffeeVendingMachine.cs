namespace CoffeeMachine.Client
{
    using System.Collections.Generic;

    public class CoffeeVendingMachine
    {
        public void AddCoffeeToOder(CoffeeSize size, int sugar, int creme)
        {
            if (ValidateCoffeeSize(size))
            {
                if (ValidateAddOnNumber(sugar) && ValidateAddOnNumber(creme))
                {
                    var coffeeOrder = new CoffeeOrder(size, sugar, creme);
                    this.CoffeeOrders.Add(coffeeOrder);
                    this.OrderTotal += coffeeOrder.GetPrice();
                }
            }
        }

        public void AddPayment(double amount)
        {
            if (this.ValidatePayment(amount))
            {
                this.CurrentPayment += amount;
            }
        }

        public List<CoffeeOrder> GetCoffeeOrders()
        {
            return this.CoffeeOrders;
        }

        public double CalculateChange()
        {
            return this.CurrentPayment - this.OrderTotal;
        }

        public double GetOrderTotal()
        {
            return this.OrderTotal;
        }

        public double GetPaymentTotal()
        {
            return this.CurrentPayment;
        }

        public bool IsAdequatePaymentProvided()
        {
            return this.CurrentPayment >= this.OrderTotal;
        }

        public static bool ValidateCoffeeSize(CoffeeSize size)
        {
            return size == CoffeeSize.Small ||
                    size == CoffeeSize.Medium ||
                    size == CoffeeSize.Large;
        }

        public static bool ValidateAddOnNumber(int value)
        {
            return value >= AddOnMinNumber && value <= AddOnMaxNumber;
        }

        public bool ValidatePayment(double amount)
        {
            return this.ValidateValueIsOfProperIncrements(amount);
        }

        public void Clear()
        {
            this.CoffeeOrders = new List<CoffeeOrder>();
            this.OrderTotal = 0.0D;
            this.CurrentPayment = 0.0D;
        }

        private bool ValidateValueIsOfProperIncrements(double amount)
        {
            var value = amount * 1000;

            return (value % 50) == 0;
        }

        public CoffeeVendingMachine()
        {
            this.CoffeeOrders = new List<CoffeeOrder>();
            this.OrderTotal = 0.0D;
            this.CurrentPayment = 0.0D;
        }

        private List<CoffeeOrder> CoffeeOrders;
        private double OrderTotal;
        private double CurrentPayment;

        private const int CoffeeMinSize = 1;
        private const int CoffeeMaxSize = 3;

        private const int AddOnMinNumber = 0;
        private const int AddOnMaxNumber = 3;
    }
}
