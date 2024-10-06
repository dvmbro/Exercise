using System.Globalization;

namespace Exercise.Domain
{
    public class Product
    {
        public Product(Guid id, string? name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public double Price { get; private set; }

        public string ToJsonString()
        {
            return $"{{" +
                $"\"{nameof(Id).ToLower()}\": \"{Id}\", " +
                $"\"{nameof(Name).ToLower()}\": \"{Name}\", " +
                $"\"{nameof(Price).ToLower()}\": {Price.ToString(CultureInfo.InvariantCulture)}" +
                $"}}";
        }
    }
}
