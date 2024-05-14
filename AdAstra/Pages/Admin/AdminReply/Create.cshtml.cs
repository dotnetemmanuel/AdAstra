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

namespace AdAstra.Pages.Admin.AdminReply
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
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["ParentReplyId"] = new SelectList(_context.Replies, "Id", "Content");
        ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Title");
            return Page();
        }

        [BindProperty]
        public Reply Reply { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                return Page();
            }
                        
            Reply.UserId = user.Id;
            _context.Replies.Add(Reply);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
