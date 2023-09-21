using Microsoft.WindowsAzure.Storage.Table;

namespace TriggerChain.Models
{
    public class Order : TableEntity
    {
        public int OrderID { get; set; }
        public string OrderName { get; set; }
        public Product Product { get; set; }
        public DateTime Sold_At { get; set; }

        public Order()
        {

        }

        public Order(int OrderID, string Name, Product Product, DateTime Sold_At)
        {
            this.OrderID = OrderID;
            this.OrderName = Name;
            this.Product = Product;
            this.Sold_At = Sold_At;


            PartitionKey = OrderID.ToString();
            RowKey = $"{OrderID}{OrderName}";
        }
    }
}
