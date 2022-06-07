namespace WebPortal_02.Models
{
    public class Products
    {
        public int id { get; set; }
        public int category_id { get; set; }
        public string product_name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public DateTime created_at { get; set; }

    }
}
