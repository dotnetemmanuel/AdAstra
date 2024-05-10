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
    public class IndexModel : PageModel
    {
        private readonly AdAstra.Data.AdAstraContext _context;

        public IndexModel(AdAstra.Data.AdAstraContext context)
        {
            _context = context;
        }

        public IList<Reply> Reply { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Reply = await _context.Replies
                .Include(r => r.Creator)
                .Include(r => r.ParentReply)
                .Include(r => r.Post).ToListAsync();
        }
    }
}
