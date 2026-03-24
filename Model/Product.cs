namespace ProductInventoryAPI.Models
{
    public class Product
    {
        public int Id { get; set;}
        public String Name { get; set;} = string.Empty;
        public String Category {get; set;} =String.Empty;
        public decimal Price {get; set;} = 0;
        public int StockQuantity {get; set;} = 0;
    }
}
