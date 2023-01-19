using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Security.Policy;
using static System.Formats.Asn1.AsnWriter;
using WebApp.Models;

namespace WebApp.Pages.Products
{


    public class EditModel : ProductCategoriesPM
    {
        private readonly WebApp.Data.WebAppContext _context;

        public EditModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product
                .Include(i => i.Admin)
                .Include(i => i.ProductCategories)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ProductID == id);

            var product = await _context.Product.FirstOrDefaultAsync(m => m.ProductID == id);
            if (Product == null)
            {
                return NotFound();
            }

            PopulateAssignedCategoryData(_context, Product);

            Product = product;
            ViewData["Category"] = new SelectList(_context.Set<Category>(), "ID", "CategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productToUpdate = await _context.Product
            .Include(i => i.Admin)
            .Include(i => i.ProductCategories)
            .FirstOrDefaultAsync(s => s.ProductID == id);
            if (productToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Product>(
                 productToUpdate,
                 "Product",
                 i => i.Name, i => i.Description, i => i.Category, i => i.Price))
            {
                UpdateProductCategories(_context, selectedCategories, productToUpdate);
                // Update other properties of the Product model
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");

            UpdateProductCategories(_context, selectedCategories, productToUpdate);

            PopulateAssignedCategoryData(_context, productToUpdate);
            return Page();
        }
    }
}