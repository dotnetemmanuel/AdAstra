using AdAstra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdAstra.Pages
{
    public class PostModel : PageModel
    {
        private readonly AdAstraContext _context;
        public readonly UserManager<Areas.Identity.Data.AdAstraUser> _userManager;

        public PostModel(AdAstraContext context, UserManager<Areas.Identity.Data.AdAstraUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Models.Post Post { get; set; }

        public async Task OnGetAsync(int postId)
        {
            if(postId != null)
            {
                Post = _context.Posts.Where(p => p.Id == postId).Include(p => p.Category).Include(p => p.Creator).Include(p => p.Replies).ThenInclude(r => r.Creator).Include(p => p.Reports).FirstOrDefault();
            }
        }
    }
}
