using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AdAstra.Data;
using AdAstra.Models;

namespace AdAstra.Pages.Admin.AdminMessage
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
        ViewData["RecipientId"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["SenderId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Message Message { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Messages.Add(Message);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
