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
using Microsoft.EntityFrameworkCore;

namespace AdAstra.Pages.Admin.AdminPost
{
    public class CreateModel : PageModel
    {
        public readonly UserManager<Areas.Identity.Data.AdAstraUser> _userManager;
        private readonly AdAstra.Data.AdAstraContext _context;


        public CreateModel(AdAstra.Data.AdAstraContext context, UserManager<Areas.Identity.Data.AdAstraUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {

            ViewData["CategoryId"] = new SelectList(_context.Categories.Where(c => c.ParentCategoryId != null), "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return Page();
        }

        [BindProperty]
        public Post Post { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            var user = await _userManager.GetUserAsync(User);
    //        var parentCategory = await _context.Categories.Where(c => c.Id == Post.CategoryId).Select(c => new
    //        {
    //            CategoryId = c.Id,
    //            CategoryName = c.Name,
    //            ParentCategoryId = c.ParentCategoryId,
    //            ParentCategoryName = c.ParentCategory != null ? c.ParentCategory.Name : null
    //        })
    //.FirstOrDefaultAsync();
            if (!ModelState.IsValid)
            {
                return Page();
            }
                        
            Post.UserId = user.Id;
            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
