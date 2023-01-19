using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Models
{
    public class ProductCategoriesPM : PageModel
    {
        public List<CategoryData> CategoryDataList;
        public void PopulateAssignedCategoryData(WebAppContext context, Product product)
        {
            var allCategories = context.Product;
            var productCategories = new HashSet<int>(
                product.ProductCategories.Select(c => c.CategoryID));

            CategoryDataList = new List<CategoryData>();
            foreach (var cat in allCategories)
            {
                CategoryDataList.Add(new CategoryData
                {
                    CategoryID = cat.ProductID,
                    Name = cat.Category,
                    Assigned = productCategories.Contains(cat.ProductID)
                });
            }
        }
        public void UpdateProductCategories(WebAppContext context, string[] selectedCategories, Product productToUpdate)
        {
            if (selectedCategories == null)
            {
                productToUpdate.ProductCategories = new List<ProductCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var productCategories = new HashSet<int>(
                productToUpdate.ProductCategories.Select(c => c.Category.Id));


            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.Id.ToString()))
                {
                    if (!productCategories.Contains(cat.Id))
                    {
                        productToUpdate.ProductCategories.Add(new ProductCategory
                        {
                            ProductID = productToUpdate.ProductID,
                            CategoryID = cat.Id
                        });
                    }
                }
                else
                {
                    if (productCategories.Contains(cat.Id))
                    {
                        ProductCategory courseToRemove = productToUpdate
                            .ProductCategories
                            .SingleOrDefault(i => i.CategoryID == cat.Id);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }

    }
}
