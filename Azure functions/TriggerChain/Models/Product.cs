namespace TriggerChain.Models
{
    public class Product
    {
        public string id { get; set; }
        public string ProductName { get; set; }

        public Product(string productID, string name)
        {
            id = productID;
            ProductName = name;
        }
    }
}
