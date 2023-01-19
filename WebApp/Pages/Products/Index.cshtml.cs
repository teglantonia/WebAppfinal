using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public IndexModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get; set; }

        public ProductDta ProductD { get; set; }
        public int ProdID { get; set; }
        public int CategoryID { get; set; }

        public string NameSort { get; set; }
        public string PriceSort { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(int? id, int? categoryID, string sortOrder)
        {
            ProductD = new ProductDta();
            NameSort = String.IsNullOrEmpty(sortOrder) ? "prod_desc" : "";
            

            ProductD.Products = await _context.Product
            .Include(b => b.ProductCategories)
            .AsNoTracking()
            .OrderBy(b => b.Name)
            .ToListAsync();

            if (id != null)
            {
                ProdID = id.Value;
                Product product = ProductD.Products
                .Where(i => i.ProductID == id.Value).Single();
                ProductD.Categories = product.ProductCategories.Select(s => s.Category);
            }
            switch (sortOrder)
            {
                case "prod_desc":
                    ProductD.Products = ProductD.Products.OrderByDescending(s =>
                   s.Name);
                    break;
            
            }

        }
    }
}
