using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdAstra.Data;
using AdAstra.Models;

namespace AdAstra.Pages.Admin.AdminCategory
{
    public class CreateModel : PageModel
    {
        private readonly AdAstra.Data.AdAstraContext _context;

        public CreateModel(AdAstra.Data.AdAstraContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var categories = _context.Categories
        .Select(c => new { c.Id, c.Name })
        .ToList();

            // Add an option for 'No Parent'
            categories.Insert(0, new { Id = (int?)null, Name = "No Parent" });

            ViewData["CreatorId"] = new SelectList(_context.Users, "Id", "UserName");
        ViewData["ParentCategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Category Category { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
