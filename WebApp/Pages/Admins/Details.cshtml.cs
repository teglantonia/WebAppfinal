using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Admins
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Data.WebAppContext _context;

        public DetailsModel(WebApp.Data.WebAppContext context)
        {
            _context = context;
        }

      public Admin Admin { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Admin == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FirstOrDefaultAsync(m => m.AdminID == id);
            if (admin == null)
            {
                return NotFound();
            }
            else 
            {
                Admin = admin;
            }
            return Page();
        }
    }
}
