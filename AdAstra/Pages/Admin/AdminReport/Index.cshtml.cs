using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdAstra.Data;
using AdAstra.Models;

namespace AdAstra.Pages.Admin.AdminReport
{
    public class IndexModel : PageModel
    {
        private readonly AdAstra.Data.AdAstraContext _context;

        public IndexModel(AdAstra.Data.AdAstraContext context)
        {
            _context = context;
        }

        public IList<Report> Report { get;set; } = default!;

        public async Task OnGetAsync()
        {          
                        Report = await _context.Reports
                .Include(r => r.ReportedPost)
                .Include(r => r.ReportedReply)
                .ThenInclude(c => c.Creator)
                .Include(r => r.Reporter).ToListAsync();
        }
    }
}
