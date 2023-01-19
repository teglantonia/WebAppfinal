namespace WebApp.Models
{
    public class ProductCategory
    {
        public int ID { get; set; }
        public int ProductID { get; set; }

        public Product Product;
        public int CategoryID { get; set; }

        public Category Category;
    }
}
