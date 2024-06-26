﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdAstra.Data;
using AdAstra.Models;

namespace AdAstra.Pages.Admin.AdminPost
{
    public class IndexModel : PageModel
    {
        private readonly AdAstra.Data.AdAstraContext _context;

        public IndexModel(AdAstra.Data.AdAstraContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Post = await _context.Posts
                .Include(p => p.Category)
                .Include(p => p.Creator)
                .Include(p => p.Replies)
                .Include(p => p.Reports)
                .ToListAsync();
        }
    }
}
