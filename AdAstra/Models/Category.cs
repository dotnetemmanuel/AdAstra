using System.ComponentModel.DataAnnotations;

namespace AdAstra.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //Nav-properties
        public int? ParentCategoryId { get; set; }
        public virtual Category? ParentCategory { get; set; }

        public string CreatorId { get; set; }
        public virtual Areas.Identity.Data.AdAstraUser Creator { get; set; }

        public virtual ICollection<Category> Subgategories { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public Category()
        {
            Subgategories = new List<Category>();
            Posts = new List<Post>();
        }

    }
}
