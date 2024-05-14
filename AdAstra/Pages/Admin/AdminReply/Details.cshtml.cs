using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdAstra.Data;
using AdAstra.Models;

namespace AdAstra.Pages.Admin.AdminReply
{
    public class DetailsModel : PageModel
    {
        private readonly AdAstra.Data.AdAstraContext _context;

        public DetailsModel(AdAstra.Data.AdAstraContext context)
        {
            _context = context;
        }

        public Reply Reply { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reply = await _context.Replies.Include(c=> c.Creator).Include(p => p.Post).Include(pr => pr.ParentReply).Include(r => r.Replies).FirstOrDefaultAsync(m => m.Id == id);
            if (reply == null)
            {
                return NotFound();
            }
            else
            {
                Reply = reply;
            }
            return Page();
        }
    }
}
