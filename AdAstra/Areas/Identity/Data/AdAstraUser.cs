﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AdAstra.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AdAstra.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AdAstraUser class
public class AdAstraUser : IdentityUser
{
    [PersonalData]
    [JsonPropertyName("birthyear")]
    public short Birthyear { get; set; }

    
    [PersonalData]
    [JsonPropertyName("firstname")]
    public string Firstname{ get; set; }

    [PersonalData]
    [JsonPropertyName("lastname")]
    public string Lastname{ get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }

    [PersonalData]
    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("categories")]
    public virtual ICollection<Category> Categories { get; set; }
    [JsonPropertyName("createdPosts")]
    public virtual ICollection<Post> CreatedPosts { get; set; }
    [JsonPropertyName("createdReplies")]
    public virtual ICollection<Reply> CreatedReplies { get; set; }
    [JsonPropertyName("sentMessages")]
    public virtual ICollection<Message> SentMessages { get; set; }
    [JsonPropertyName("receivedMessages")]
    public virtual ICollection<Message> ReceivedMessages { get; set; }
    [JsonPropertyName("reportsMade")]
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

