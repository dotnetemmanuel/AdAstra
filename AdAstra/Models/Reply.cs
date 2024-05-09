using System.ComponentModel.DataAnnotations;

namespace AdAstra.Models
{
    public class Reply
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Reply")]
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Likes { get; set; }

        //Nav-properties
        public int PostId { get; set; }
        public Post Post { get; set; }

        public string UserId { get; set; }
        public virtual Areas.Identity.Data.AdAstraUser Creator { get; set; }

        public int? ParentReplyId { get; set; }
        public virtual Reply ParentReply { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }

        public virtual ICollection<Report> Reports { get; set; }

        public Reply()
        {
            Replies = new List<Reply>();
            Reports = new List<Report>();
            CreatedAt = DateTime.Now;
        }
    }
}
