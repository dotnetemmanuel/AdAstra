using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdAstra.Data;
using AdAstra.Models;
using Microsoft.AspNetCore.Identity;

namespace AdAstra.Pages.Admin.AdminCategory
{
    public class CreateModel : PageModel
    {
        public readonly UserManager<AdAstra.Areas.Identity.Data.AdAstraUser> _userManager;

        private readonly AdAstra.Data.AdAstraContext _context;

        public CreateModel(AdAstra.Data.AdAstraContext context, UserManager<AdAstra.Areas.Identity.Data.AdAstraUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
            var user = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Category.CreatorId = user.Id;
            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
