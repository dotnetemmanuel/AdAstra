using AdAstra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdAstra.Pages
{
    public class SearchResultsModel : PageModel
    {
        private readonly AdAstraContext _context;

        public SearchResultsModel(AdAstraContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchPrompt { get; set; }

        public List<Models.Post> Results { get; set; }

        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(SearchPrompt))
            {
                Results = await SearchDatabase(SearchPrompt);
            }
        }

        private async Task<List<Models.Post>> SearchDatabase(string searchPrompt)
        {
            var searchWords = searchPrompt.ToLower().Split(' ');
            
            var allPosts =  await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.Replies)
                .Include(p => p.Reports)
                .ToListAsync();
            
            var results = allPosts
                .Where(post => searchWords.Any(word => post.Title.ToLower().Contains(word)))
                .ToList();

            return results;
        }
    }
}
