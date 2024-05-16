using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace AdAstra.Models
{
    public class Post
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Title")]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Post")]
        [JsonPropertyName("content")]
        public string Content { get; set; }
        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("likes")]
        public int Likes { get; set; }

        //Nav-properties
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }
        [JsonPropertyName("category")]
        public Category? Category { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }
        [JsonPropertyName("creator")]
        public Areas.Identity.Data.AdAstraUser? Creator { get; set; }

        [JsonPropertyName("replies")]
        public virtual ICollection<Reply> Replies { get; set; }
        [JsonPropertyName("reports")]
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
