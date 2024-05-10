using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdAstra.Data;
using AdAstra.Models;

namespace AdAstra.Pages.Admin.AdminReport
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Reports.Add(Report);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
