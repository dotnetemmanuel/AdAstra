using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdAstra.Data;
using AdAstra.Models;

namespace AdAstra.Pages.Admin.AdminPost
{
    public class DetailsModel : PageModel
    {
        private readonly AdAstra.Data.AdAstraContext _context;

        public DetailsModel(AdAstra.Data.AdAstraContext context)
        {
            _context = context;
        }

        public Post Post { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            else
            {
                Post = post;
            }
            return Page();
        }
    }
}
