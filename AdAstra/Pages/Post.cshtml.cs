using AdAstra.Data;
using AdAstra.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;

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
        public Models.Report Report { get; set; }
        public Models.Reply Reply { get; set; }
        public Areas.Identity.Data.AdAstraUser AdastraUser { get; set; }


        public async Task OnGetAsync(int postId)
        {
            if (postId != null)
            {
                Post = _context.Posts.Where(p => p.Id == postId).Include(p => p.Category).Include(p => p.Reports).Include(p => p.Creator).Include(p => p.Replies).ThenInclude(r => r.Reports).Include(p => p.Replies).ThenInclude(r => r.Creator).FirstOrDefault();
            }
        }

        public async Task<IActionResult> OnPostLikePostAsync(int postId)
        {
            Post = _context.Posts.Where(p => p.Id == postId).Include(p => p.Category).Include(p => p.Creator).Include(p => p.Replies).ThenInclude(r => r.Creator).Include(p => p.Reports).FirstOrDefault();

            
            if (Post is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Post.Likes++;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Post", new { postId = Post.Id });
        }

        public async Task<IActionResult> OnPostReportReplyAsync(int id, int postId)
        {
            if (User.Identity.IsAuthenticated)
            {
                Post = _context.Posts.Where(p => p.Id == postId).Include(p => p.Category).Include(p => p.Creator).Include(p => p.Replies).ThenInclude(r => r.Creator).Include(p => p.Reports).FirstOrDefault();

                Reply = Post.Replies.Where(r => r.Id == id).FirstOrDefault();

                if (Reply is null)
                {
                    return NotFound();
                }
                                
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if(!ModelState.IsValid)
                {
                    return Page();
                }
                Report = new();
                Report.ReporterId = userId;
                Report.PostId = Post.Id;
                Report.ReplyId = id;

                Post.Reports.Add(Report);
                await _context.SaveChangesAsync();
                               
            }
            return RedirectToPage("/Post", new { postId = Post.Id });
        }

        public async Task<IActionResult> OnPostLikeReplyAsync(int id, int postId)
        {
            Post = _context.Posts.Where(p => p.Id == postId).Include(p => p.Category).Include(p => p.Creator).Include(p => p.Replies).ThenInclude(r => r.Creator).Include(p => p.Reports).FirstOrDefault();

            Reply = Post.Replies.Where(r => r.Id == id).FirstOrDefault();

            if (Reply is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Reply.Likes++;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Post", new { postId = Post.Id });
        }
    }
}
