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
        public List<Models.Post> Posts { get; set; }

        public readonly UserManager<Areas.Identity.Data.AdAstraUser> _userManager;
        private readonly AdAstraContext _context;

        public IndexModel(UserManager<Areas.Identity.Data.AdAstraUser> userManager, AdAstraContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Categories = await DAL.AdAstraApi.GetAllCategoriesFromAPI();
            Users = await _userManager.Users.ToListAsync();
            Posts = await _context.Posts.Include(p => p.Creator).Include(p => p.Replies).Include(p => p.Reports).ToListAsync();
        }
    }
}
