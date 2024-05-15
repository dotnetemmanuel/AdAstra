using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdAstra.Data;
using AdAstra.Models;
using Microsoft.AspNetCore.Identity;

namespace AdAstra.Pages.Admin.AdminMessage
{
    public class IndexModel : PageModel
    {
        public readonly UserManager<Areas.Identity.Data.AdAstraUser> _userManager;
        private readonly AdAstra.Data.AdAstraContext _context;

        public IndexModel(AdAstra.Data.AdAstraContext context, UserManager<Areas.Identity.Data.AdAstraUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
