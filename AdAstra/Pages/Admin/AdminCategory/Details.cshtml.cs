using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdAstra.Data;
using AdAstra.Models;
using Microsoft.AspNetCore.Identity;

namespace AdAstra.Pages.Admin.AdminCategory
{
    public class DetailsModel : PageModel
    {
        private readonly AdAstra.Data.AdAstraContext _context;

        public DetailsModel(AdAstra.Data.AdAstraContext context)
        {
            _context = context;            
        }

        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {            
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.Include(c=> c.Creator).Include(pc => pc.ParentCategory).FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                Category = category;
                
            }
            return Page();
        }
    }
}
