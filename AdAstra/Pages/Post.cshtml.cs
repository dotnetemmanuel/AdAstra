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
        public List<Models.Report> Reports { get; set; }
        public Models.Reply Reply { get; set; }
        public Models.Reply Subreply { get; set; }
        public Areas.Identity.Data.AdAstraUser AdastraUser { get; set; }
        public Models.Category Category { get; set; }

        [BindProperty]
        public Models.Reply PostReply { get; set; }

        [BindProperty]
        public Models.Reply ReplySubreply { get; set; }

        [BindProperty]
        public Models.Reply SubreplySubreply { get; set; }


        public async Task OnGetAsync(int postId)
        {
            if (postId != null)
            {
                Post = _context.Posts.Where(p => p.Id == postId)
                    .Include(p => p.Category)
                    .Include(p => p.Reports)
                    .Include(p => p.Creator)
                    .Include(p => p.Replies).ThenInclude(r => r.Reports)
                    .Include(p => p.Replies).ThenInclude(r => r.Creator)
                    .Include(p => p.Replies).ThenInclude(r => r.Replies).ThenInclude(r => r.Reports)
                    .FirstOrDefault();

                Reports = Post.Reports.Where(r => r.ReplyId == null).ToList();
            }
        }

        public async Task<IActionResult> OnPostCreateReplyAsync(int postId, string userId, string content)
        {
            Post = _context.Posts
                .Where(p => p.Id == postId)
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.Replies).ThenInclude(r => r.Creator)
                .Include(p => p.Reports)
                .FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return Page();
            }


            PostReply = new()
            {
                UserId = userId,
                PostId = postId,
                Content = PostReply.Content
            };
            //PostReply.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Replies.Add(PostReply);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Post", new { postId = PostReply.PostId });
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

        public async Task<IActionResult> OnPostReportPostAsync(int postId)
        {
            Post = _context.Posts.Where(p => p.Id == postId)
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.Replies).ThenInclude(r => r.Creator)
                .Include(r => r.Replies).ThenInclude(r => r.Reports)
                .Include(p => p.Reports).FirstOrDefault();

            if (Post is null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Report = new();
            Report.ReporterId = userId;
            Report.PostId = Post.Id;

            Post.Reports.Add(Report);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Post", new { postId = Post.Id });
        }

        public async Task<IActionResult> OnPostCreateSubreplyAsync(int postId, string userId, int replyId, string content)
        {
            Post = _context.Posts.Where(p => p.Id == postId)
               .Include(p => p.Category)
               .Include(p => p.Creator)
               .Include(p => p.Replies).ThenInclude(r => r.Creator)
               .Include(r => r.Replies).ThenInclude(r => r.Reports)
               .Include(p => p.Reports).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            ReplySubreply = new()
            {
                PostId = postId,
                UserId = userId,
                ParentReplyId = replyId,
                Content = ReplySubreply.Content
            };

            _context.Replies.Add(ReplySubreply);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Post", new { postId = ReplySubreply.PostId });
        }

        public async Task<IActionResult> OnPostLikeReplyAsync(int id, int postId)
        {
            Post = _context.Posts.Where(p => p.Id == postId)
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.Replies).ThenInclude(r => r.Creator)
                .Include(p => p.Reports).FirstOrDefault();

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

        public async Task<IActionResult> OnPostReportReplyAsync(int id, int postId)
        {
            if (User.Identity.IsAuthenticated)
            {
                Post = _context.Posts.Where(p => p.Id == postId)
                    .Include(p => p.Category)
                    .Include(p => p.Creator)
                    .Include(p => p.Replies).ThenInclude(r => r.Creator)
                    .Include(p => p.Reports)
                    .FirstOrDefault();

                Reply = Post.Replies.Where(r => r.Id == id).FirstOrDefault();

                if (Reply is null)
                {
                    return NotFound();
                }

                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!ModelState.IsValid)
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

        public async Task<IActionResult> OnPostCreateSubreplySubreplyAsync(int postId, string userId, int subreplyId, string content)
        {
            Post = _context.Posts.Where(p => p.Id == postId)
               .Include(p => p.Category)
               .Include(p => p.Creator)
               .Include(p => p.Replies).ThenInclude(r => r.Creator)
               .Include(r => r.Replies).ThenInclude(r => r.Reports)
               .Include(p => p.Reports).FirstOrDefault();
                        

            if (!ModelState.IsValid)
            {
                return Page();
            }

            SubreplySubreply = new()
            {
                PostId = postId,
                UserId = userId,
                ParentReplyId = subreplyId,
                Content = SubreplySubreply.Content
            };

            _context.Replies.Add(SubreplySubreply);

            await _context.SaveChangesAsync();

            return RedirectToPage("/Post", new { postId = SubreplySubreply.PostId });
        }

        public async Task<IActionResult> OnPostLikeSubreplyAsync(int replyId, int subreplyId, int postId)
        {
            Post = _context.Posts.Where(p => p.Id == postId)
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.Replies).ThenInclude(r => r.Creator).
                Include(p => p.Reports)
                .Include(p => p.Replies).ThenInclude(r => r.Replies)
                .FirstOrDefault();

            Reply = Post.Replies.Where(r => r.Id == replyId).FirstOrDefault();
            Subreply = Reply.Replies.Where(sr => sr.Id == subreplyId).FirstOrDefault();

            if (Subreply is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Subreply.Likes++;
            await _context.SaveChangesAsync();

            return RedirectToPage("/Post", new { postId = Post.Id });
        }

        public async Task<IActionResult> OnPostReportSubreplyAsync(int postId, int replyId, int subreplyId)
        {
            Post = _context.Posts.Where(p => p.Id == postId)
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.Replies).ThenInclude(r => r.Creator)
                .Include(p => p.Reports)
                .Include(p => p.Replies).ThenInclude(r => r.Replies)
                .FirstOrDefault();
            Reply = Post.Replies.Where(r => r.Id == replyId).FirstOrDefault();
            Subreply = Reply.Replies.Where(sr => sr.Id == subreplyId).FirstOrDefault();

            if (Subreply is null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Report = new();
            Report.ReporterId = userId;
            Report.PostId = Post.Id;
            Report.ReplyId = subreplyId;

            Post.Reports.Add(Report);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Post", new { postId = Post.Id });
        }
    }
}
