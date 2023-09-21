namespace TriggerChain.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }

        public Product(int id, string name, bool orderStatus)
        {
            ProductID = id;
            ProductName = name;
        }
    }
}
