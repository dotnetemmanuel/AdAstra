using System.ComponentModel.DataAnnotations;

namespace AdAstra.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Post")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Likes { get; set; }

        //Nav-properties
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public string UserId { get; set; }
        public Areas.Identity.Data.AdAstraUser? Creator { get; set; }

        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Report> Reports { get; set; }

        public Post()
        {
            Replies = new List<Reply>();
            Reports = new List<Report>();
            CreatedAt = DateTime.Now;
            Likes = 0;
        }
    }
}
