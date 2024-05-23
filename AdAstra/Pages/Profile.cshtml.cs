using AdAstra.Data;
using AdAstra.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AdAstra.Pages
{
    public class ProfileModel : PageModel
    {
        public readonly UserManager<Areas.Identity.Data.AdAstraUser> _userManager;
        private readonly AdAstraContext _context;

        public ProfileModel(AdAstraContext context, UserManager<Areas.Identity.Data.AdAstraUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Areas.Identity.Data.AdAstraUser User { get; set; }

        [BindProperty]
        public Models.Message Message { get; set; }

        public async Task OnGet(string userId)
        {
            User = _userManager.Users.
                Where(u => u.Id == userId)
                .Include(u => u.CreatedPosts)
                .Include(u => u.CreatedReplies)
                .FirstOrDefault();
        }

        public async Task<IActionResult> OnPost(string senderId, string recipientId)
        {
            var sender = _userManager.Users.Where(u => u.Id == senderId).FirstOrDefault();
            var recipient = _userManager.Users.Where(u => u.Id == recipientId).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                return Page();
            }                       

            Message.SenderId = senderId;
            Message.RecipientId = recipientId;

            sender.SentMessages.Add(Message);
            recipient.ReceivedMessages.Add(Message);

            await _context.SaveChangesAsync();
            return RedirectToPage("/Profile", new { userId = recipient.Id });

        }
    }
}
