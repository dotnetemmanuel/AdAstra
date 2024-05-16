using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdAstra.DAL
{
    public class AdAstraApi
    {
        public static Uri BaseAddress = new Uri("https://localhost:44310/");

        public static async Task<List<Models.Category>> GetAllCategoriesFromAPI()
        {
            List<Models.Category> categories = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpResponseMessage response = await client.GetAsync("api/Categories");
                if(response.IsSuccessStatusCode)
                {
                    string responseStr = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        ReferenceHandler = ReferenceHandler.Preserve,
                        PropertyNameCaseInsensitive = true
                    };
                    categories = JsonSerializer.Deserialize<List<Models.Category>>(responseStr, options);
                }
                return categories;
            }
        }

        public static async Task<Models.Category> GetOneCategoryFromAPI(int id)
        {
           Models.Category category = new();

            using (var client = new HttpClient())
            {
                client.BaseAddress = BaseAddress;
                HttpResponseMessage response = await client.GetAsync("api/Categories/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string responseStr = await response.Content.ReadAsStringAsync();
                    category = JsonSerializer.Deserialize<Models.Category>(responseStr);
                }
                return category;
            }
        }
    }
}
