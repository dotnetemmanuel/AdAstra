using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdAstra.Pages
{
    public class IndexModel : PageModel
    {
        public List<Areas.Identity.Data.AdAstraUser> Users { get; set; }
        public readonly UserManager<Areas.Identity.Data.AdAstraUser> _userManager;

        public IndexModel(UserManager<Areas.Identity.Data.AdAstraUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
        }
    }
}
