namespace CoffeeMachine.Client.Tests
{
    using Xunit;

    public class CoffeeOrderTests
    {
        [Fact]
        public void GetCoffeeSize_SmallCoffee_Valid()
        {
            Assert.Equal(CoffeeSize.Small, CoffeeOrder.GetCoffeeSize(ValidSamllCoffeeCode));
        }

        [Fact]
        public void GetCoffeeSize_MediumCoffee_Valid()
        {
            Assert.Equal(CoffeeSize.Medium, CoffeeOrder.GetCoffeeSize(ValidMediumCoffeeCode));
        }

        [Fact]
        public void GetCoffeeSize_LargeCoffee_Valid()
        {
            Assert.Equal(CoffeeSize.Large, CoffeeOrder.GetCoffeeSize(ValidLargeCoffeeCode));
        }

        [Fact]
        public void GetCoffeeSize_InvalidCoffeeCode_InvalidCode()
        {
            Assert.Equal(CoffeeSize.Invalid, CoffeeOrder.GetCoffeeSize(InvalidCoffeeCode));
        }

        [Fact]
        public void GetPrice_SmallCoffeeNoAddOns_CorrectPrice()
        {
            var coffeeOrder = new CoffeeOrder(CoffeeSize.Small, 0, 0);

            Assert.Equal(CoffeeOrder.SmallCoffeePrice, coffeeOrder.GetPrice());
        }

        [Fact]
        public void GetPrice_MediumCoffeeNoAddOns_CorrectPrice()
        {
            var coffeeOrder = new CoffeeOrder(CoffeeSize.Medium, 0, 0);

            Assert.Equal(CoffeeOrder.MediumCoffeePrice, coffeeOrder.GetPrice());
        }

        [Fact]
        public void GetPrice_LargeCoffeeNoAddOns_CorrectPrice()
        {
            var coffeeOrder = new CoffeeOrder(CoffeeSize.Large, 0, 0);

            Assert.Equal(CoffeeOrder.LargeCoffeePrice, coffeeOrder.GetPrice());
        }

        [Fact]
        public void GetPrice_MediumCoffeeOneCreamOneSugar_CorrectPrice()
        {
            var coffeeOrder = new CoffeeOrder(CoffeeSize.Medium, 1, 1);

            Assert.Equal(CoffeeOrder.MediumCoffeePrice + CoffeeOrder.SugarPrice + CoffeeOrder.CreamerPrice, coffeeOrder.GetPrice());
        }

        private const string ValidSamllCoffeeCode = "S";
        private const string ValidMediumCoffeeCode = "M";
        private const string ValidLargeCoffeeCode = "L";

        private const string InvalidCoffeeCode = "h";
    }
}
