namespace Day2
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Name) && Price > 0;
        }

    }
}
