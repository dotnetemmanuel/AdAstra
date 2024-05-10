using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdAstra.Data;
using AdAstra.Models;

namespace AdAstra.Pages.Admin.AdminMessage
{
    public class IndexModel : PageModel
    {
        private readonly AdAstra.Data.AdAstraContext _context;

        public IndexModel(AdAstra.Data.AdAstraContext context)
        {
            _context = context;
        }

        public IList<Message> Message { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Message = await _context.Messages
                .Include(m => m.Recepient)
                .Include(m => m.Sender).ToListAsync();
        }
    }
}
