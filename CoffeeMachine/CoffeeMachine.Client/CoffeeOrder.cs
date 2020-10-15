namespace CoffeeMachine.Client
{
    public class CoffeeOrder
    {
        public double GetPrice()
        {
            double price = GetCoffeePrice();
                        
            price += this.NumberOfSugars * SugarPrice;
            price += this.NumberOfCreams * CreamerPrice;

            return price;
        }

        public static CoffeeSize GetCoffeeSize(string size)
        {
            switch(size.ToUpperInvariant())
            {
                case "S":
                    return CoffeeSize.Small;
                case "M":
                    return CoffeeSize.Medium;
                case "L":
                    return CoffeeSize.Large;
                default:
                    return CoffeeSize.Invalid;
            }
        }

        private double GetCoffeePrice()
        {
            switch (this.Size)
            {
                case CoffeeSize.Small:
                    return SmallCoffeePrice;
                case CoffeeSize.Medium:
                    return MediumCoffeePrice;
                case CoffeeSize.Large:
                    return LargeCoffeePrice;
                default:
                    return 0.0D;
            }
        }

        public CoffeeOrder(CoffeeSize size, int sugars, int creams)
        {
            this.Size = size;
            this.NumberOfSugars = sugars;
            this.NumberOfCreams = creams;
        }

        public CoffeeSize Size { get; set; }
        public int NumberOfSugars { get; set; }
        public int NumberOfCreams { get; set; }

        public const double SmallCoffeePrice = 1.75D;
        public const double MediumCoffeePrice = 2.0D;
        public const double LargeCoffeePrice = 2.25D;

        public const double SugarPrice = 0.25D;
        public const double CreamerPrice = 0.50D;
    }
}
