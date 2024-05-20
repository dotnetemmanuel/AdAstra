using AdAstra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdAstra.Pages
{
    public class PostsModel : PageModel
    {
       
        private readonly AdAstraContext _context;

        public PostsModel(AdAstraContext context)
        {
            _context = context;
        }

        public List<Models.Post> Posts { get; set; }
        public Models.Category Category { get; set; }

        public async Task OnGetAsync(int subcategoryId)
        {
            if(subcategoryId != null)
            {
                Category = _context.Categories.Where(c => c.Id == subcategoryId).FirstOrDefault();
                Posts = await _context.Posts.Where(p => p.CategoryId == subcategoryId).Include(p => p.Category).Include(p=> p.Creator).Include(p => p.Replies).Include(p => p.Reports).ToListAsync();
            }            
        }
    }
}
