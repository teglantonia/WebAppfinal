using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebApp.Models
{
    public class Admin
    {
        public int AdminID { get; set; }

        [Display(Name = "Semnatura editor")]
        public string Name { get; set; }

        [Display(Name = "Modificari aduse stie-ului ")]
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}