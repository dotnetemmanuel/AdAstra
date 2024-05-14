namespace AdAstra.Models
{
    public class Report
    {
        public int Id { get; set; }
        public DateTime ReportedAt { get; set; }

        public string ReporterId { get; set; }
        public Areas.Identity.Data.AdAstraUser? Reporter { get; set; }

        public int? PostId { get; set; }
        public Post? ReportedPost { get; set; }

        public int? ReplyId { get; set; }
        public Reply? ReportedReply { get; set; }

        public Report()
        {
            ReportedAt = DateTime.Now;
        }
    }
}
