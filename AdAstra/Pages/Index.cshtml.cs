using AdAstra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdAstra.Pages
{
    public class IndexModel : PageModel
    {
        public List<Areas.Identity.Data.AdAstraUser> Users { get; set; }
        public List<Models.Category> Categories { get; set; }

        public readonly UserManager<Areas.Identity.Data.AdAstraUser> _userManager;
        private readonly AdAstraContext _context;

        public IndexModel(UserManager<Areas.Identity.Data.AdAstraUser> userManager, AdAstraContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
            Categories = await _context.Categories.Where(c => c.ParentCategory == null).Include(sc => sc.Subgategories).ToListAsync();
        }
    }
}
