﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AdAstra.Areas.Identity.Data;
using AdAstra.Data;
using AdAstra.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdAstra.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<AdAstraUser> _userManager;
        private readonly SignInManager<AdAstraUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly AdAstraContext _context;

        public DeletePersonalDataModel(
            UserManager<AdAstraUser> userManager,
            SignInManager<AdAstraUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            AdAstraContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password.");
                    return Page();
                }
            }

            //var result = await _userManager.DeleteAsync(user);
            //var userId = await _userManager.GetUserIdAsync(user);
            var userId = await _userManager.GetUserIdAsync(user);
            var result = await DeleteUserAsync(userId);


            //if (!result.Succeeded)
            if (!result)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }

        //DELETE ALL USER-RELATED DATA TO DELETE USER
        public async Task<bool> DeleteUserAsync(string userId)        {
            
            var user = await _context.Users
                .Include(u => u.CreatedPosts)
                    .ThenInclude(p => p.Replies)
                        .ThenInclude(r => r.Replies)
                .Include(u => u.CreatedReplies)
                    .ThenInclude(r => r.Replies)
                .Include(u => u.SentMessages)
                .Include(u => u.ReceivedMessages)
                .Include(u => u.ReportsMade)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }
                        
            foreach (var reply in user.CreatedReplies)
            {
                var relatedReports = _context.Reports.Where(r => r.ReplyId == reply.Id);
                _context.Reports.RemoveRange(relatedReports);
            }
                        
            foreach (var post in user.CreatedPosts)
            {
                var relatedReports = _context.Reports.Where(r => r.PostId == post.Id);
                _context.Reports.RemoveRange(relatedReports);
            }
                        
            foreach (var post in user.CreatedPosts)
            {
                DeleteReplies(post.Replies);
            }
                       
            DeleteReplies(user.CreatedReplies);

            _context.Messages.RemoveRange(user.SentMessages);
            _context.Messages.RemoveRange(user.ReceivedMessages);

            _context.Reports.RemoveRange(user.ReportsMade);

            _context.Posts.RemoveRange(user.CreatedPosts);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return true;
        }


        private void DeleteReplies(ICollection<Reply> replies)
        {
            foreach (var reply in replies)
            {
                if (reply.Replies != null && reply.Replies.Any())
                {
                    DeleteReplies(reply.Replies);
                }
                _context.Replies.Remove(reply);
            }
        }
    }
}
