using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AdAstra.Pages.AdminRole
{
    public class IndexModel : PageModel
    {
        public List<Areas.Identity.Data.AdAstraUser> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RoleName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string RemoveUserId { get; set; }

        public bool IsUser { get; set; }
        public bool IsAdmin { get; set; }        
        public bool IsHiggs {  get; set; }

        public readonly UserManager<Areas.Identity.Data.AdAstraUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public IndexModel(UserManager<Areas.Identity.Data.AdAstraUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task OnGetAsync()
        {
            Users = await _userManager.Users.ToListAsync();
            Roles = await _roleManager.Roles.ToListAsync();

            if(AddUserId is not null)
            {
                var alterUser = await _userManager.FindByIdAsync(AddUserId);
                await _userManager.AddToRoleAsync(alterUser, RoleName);
            }
            if(RemoveUserId is not null)
            {
                var alterUser = await _userManager.FindByIdAsync(RemoveUserId);
                await _userManager.RemoveFromRoleAsync(alterUser, RoleName);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if(currentUser is not null)
            {
                IsUser = await _userManager.IsInRoleAsync(currentUser, "User");
                IsAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");
                IsHiggs = await _userManager.IsInRoleAsync(currentUser, "Higgs");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(RoleName is not null)
            {
                await CreateRole(RoleName);
            }
            return RedirectToPage("./Index");
        }

        public async Task CreateRole(string roleName)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);
            if(!roleExists)
            {
                var Role = new IdentityRole
                {
                    Name = roleName
                };
                await _roleManager.CreateAsync(Role);
            }
        }
    }
}
