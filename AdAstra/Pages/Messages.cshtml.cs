using AdAstra.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdAstra.Pages
{
    public class MessagesModel : PageModel
    {
        public readonly UserManager<Areas.Identity.Data.AdAstraUser> _userManager;
        private readonly AdAstraContext _context;

        public MessagesModel(AdAstraContext context, UserManager<Areas.Identity.Data.AdAstraUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Areas.Identity.Data.AdAstraUser User { get; set; }
        public List<Models.Message> Sent { get; set; }
        public List<Models.Message> Received { get; set; }

        public async Task OnGet(string userId)
        {
            User = _userManager.Users
               .Where(u => u.Id == userId)
               .Include(u => u.CreatedPosts)
               .Include(u => u.CreatedReplies)
               .Include(u => u.ReceivedMessages).ThenInclude(u => u.Sender)               
               .Include(u => u.SentMessages).ThenInclude(u => u.Recepient)
               .FirstOrDefault();

            Received = User.ReceivedMessages.ToList();
            Sent = User.SentMessages.ToList();
        }
    }
}
