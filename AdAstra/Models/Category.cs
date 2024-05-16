using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdAstra.Models
{
    public class Category
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [Required]
        [Display(Name = "Category")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        [JsonPropertyName("description")]
        public string Description { get; set; }

        //Nav-properties
        [JsonPropertyName("parentCategoryId")]
        public int? ParentCategoryId { get; set; }
        [JsonPropertyName("parentCategory")]
        public Category? ParentCategory { get; set; }

        [JsonPropertyName("creatorId")]
        public string CreatorId { get; set; }
        [JsonPropertyName("creator")]
        public Areas.Identity.Data.AdAstraUser? Creator { get; set; }

        [JsonPropertyName("subgategories")]
        public virtual ICollection<Category> Subgategories { get; set; }
        [JsonPropertyName("posts")]
        public virtual ICollection<Post> Posts { get; set; }

        public Category()
        {
            Subgategories = new List<Category>();
            Posts = new List<Post>();
        }

    }
}
