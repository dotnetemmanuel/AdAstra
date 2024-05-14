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

namespace AdAstra.Pages.Admin.AdminReport
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
            var replies = _context.Categories
        .Select(c => new { c.Id, c.Name })
        .ToList();

            replies.Insert(0, new { Id = (int?)null, Name = "No Parent" });

            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Content");
        ViewData["ReplyId"] = new SelectList(_context.Replies, "Id", "Content");
        ViewData["ReporterId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Report Report { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Report.ReporterId = user.Id;
            _context.Reports.Add(Report);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
