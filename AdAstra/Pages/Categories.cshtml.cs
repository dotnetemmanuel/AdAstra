using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdAstra.Pages
{
    public class CategoriesModel : PageModel
    {
        public List<Models.Category> Categories { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await DAL.AdAstraApi.GetAllCategoriesFromAPI();
        }
    }
}
