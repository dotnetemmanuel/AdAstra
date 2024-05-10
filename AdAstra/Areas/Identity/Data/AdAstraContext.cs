using AdAstra.Areas.Identity.Data;
using AdAstra.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AdAstra.Data;

public class AdAstraContext : IdentityDbContext<AdAstraUser>
{
    public AdAstraContext(DbContextOptions<AdAstraContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<Reply> Replies { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>()
        .HasOne(p => p.Creator)
        .WithMany(u => u.CreatedPosts)
        .HasForeignKey(p => p.UserId);

        builder.Entity<Post>()
        .HasOne(p => p.Category)
        .WithMany(c => c.Posts)
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.Restrict); // Change the delete behavior to Restrict

        builder.Entity<Reply>()
        .HasOne(r => r.Post)
        .WithMany(p => p.Replies)
        .HasForeignKey(r => r.PostId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Reply>()
        .HasOne(r => r.ParentReply)
        .WithMany(r => r.Replies)
        .HasForeignKey(r => r.ParentReplyId)
        .IsRequired(false);

        builder.Entity<Category>()
        .HasOne(c => c.ParentCategory)
        .WithMany(c => c.Subgategories)
        .HasForeignKey(c => c.ParentCategoryId)
        .IsRequired(false);

        builder.Entity<Category>()
        .HasOne(c => c.Creator) 
        .WithMany(c=>c.Categories)
        .HasForeignKey(c => c.CreatorId)
        .IsRequired(false);

        builder.Entity<Report>()
        .HasOne(r => r.ReportedPost)
        .WithMany(p => p.Reports)
        .HasForeignKey(r => r.PostId)
        .IsRequired(false);

        builder.Entity<Report>()
        .HasOne(r => r.ReportedReply)
        .WithMany(r => r.Reports)
        .HasForeignKey(r => r.ReplyId)
        .IsRequired(false);

        builder.Entity<Message>()
        .HasOne(m => m.Sender)
        .WithMany(u => u.SentMessages)
        .HasForeignKey(m => m.SenderId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Message>()
        .HasOne(m => m.Recepient)
        .WithMany(u => u.ReceivedMessages)
        .HasForeignKey(m => m.RecipientId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdAstraUser>()
        .HasMany(u => u.CreatedPosts)
        .WithOne(p => p.Creator)
        .HasForeignKey(p => p.UserId);

        builder.Entity<AdAstraUser>()
        .HasMany(u => u.CreatedReplies)
        .WithOne(r => r.Creator)
        .HasForeignKey(r => r.UserId);

        builder.Entity<AdAstraUser>()
        .HasMany(u => u.SentMessages)
        .WithOne(m => m.Sender)
        .HasForeignKey(m => m.SenderId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdAstraUser>()
        .HasMany(u => u.ReceivedMessages)
        .WithOne(m => m.Recepient)
        .HasForeignKey(m => m.RecipientId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<AdAstraUser>()
        .HasMany(u => u.ReportsMade)
        .WithOne(r => r.Reporter)
        .HasForeignKey(r => r.ReporterId);
    }
}
