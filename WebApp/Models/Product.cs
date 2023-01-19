using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApp.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        [Display(Name = "Product")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [Column(TypeName = "decimal(6,2)")]
        public decimal Price { get; set; }
        public Admin? Admin { get; set; }
        public ICollection<ProductCategory>? ProductCategories { get; set; }
    }
}