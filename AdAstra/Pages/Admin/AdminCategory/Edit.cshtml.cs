using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdAstra.Data;
using AdAstra.Models;

namespace AdAstra.Pages.Admin.AdminCategory
{
    public class EditModel : PageModel
    {
        private readonly AdAstra.Data.AdAstraContext _context;

        public EditModel(AdAstra.Data.AdAstraContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categories = _context.Categories
        .Select(c => new { c.Id, c.Name })
        .ToList();

            categories.Insert(0, new { Id = (int?)null, Name = "No Parent" });

            var category =  await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            Category = category;
           ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "UserName");
           ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Description");
            return Page();
        }
                
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(Category.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CategoryExists(int? id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
