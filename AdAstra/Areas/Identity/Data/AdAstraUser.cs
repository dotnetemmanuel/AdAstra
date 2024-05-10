using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AdAstra.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AdAstra.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AdAstraUser class
public class AdAstraUser : IdentityUser
{
    [PersonalData]
    public short Birthyear { get; set; }

    [PersonalData]
    public string Firstname{ get; set; }

    [PersonalData]
    public string Lastname{ get; set; }

    public string Avatar { get; set; }

    [PersonalData]
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Post> CreatedPosts { get; set; }
    public virtual ICollection<Reply> CreatedReplies { get; set; }
    public virtual ICollection<Message> SentMessages { get; set; }
    public virtual ICollection<Message> ReceivedMessages { get; set; }
    public virtual ICollection<Report> ReportsMade { get; set; }
    

    public AdAstraUser()
    {
        Categories = new List<Category>();
        CreatedPosts = new List<Post>();
        CreatedReplies = new List<Reply>();
        SentMessages = new List<Message>();
        ReceivedMessages = new List<Message>();
        ReportsMade = new List<Report>();
        CreatedAt = DateTime.Now;
        Avatar = "~/img/user.png";
    }
}

