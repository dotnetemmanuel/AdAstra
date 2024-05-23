using AdAstra.Areas.Identity.Data;
using AdAstra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AdAstra.Pages
{
    public class PostsModel : PageModel
    {
       
        private readonly AdAstraContext _context;
        public readonly UserManager<Areas.Identity.Data.AdAstraUser> _userManager;

        public PostsModel(AdAstraContext context, UserManager<Areas.Identity.Data.AdAstraUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Models.Post> Posts { get; set; }
        public Models.Category Category { get; set; }
       
        [BindProperty]
        public Models.Post Post { get; set; }

        public async Task OnGetAsync(int subcategoryId)
        {
            if(subcategoryId != null)
            {
                Category = _context.Categories.Where(c => c.Id == subcategoryId).FirstOrDefault();
                Posts = await _context.Posts.Where(p => p.CategoryId == subcategoryId)
                    .Include(p => p.Category)
                    .Include(p=> p.Creator)
                    .Include(p => p.Replies)
                    .Include(p => p.Reports)
                    .ToListAsync();                
            }            
        }

        public async Task<IActionResult> OnPostAsync()
        {                    
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Post.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Posts.Add(Post);

            await _context.SaveChangesAsync();
            return RedirectToPage("/Posts", new { subcategoryId = Post.CategoryId });                        
        }
    }
}
