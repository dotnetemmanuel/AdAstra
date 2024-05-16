using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AdAstra.Data;
namespace AdAstra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("AdAstraContextConnection") ?? throw new InvalidOperationException("Connection string 'AdAstraContextConnection' not found.");

            builder.Services.AddDbContext<AdAstraContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<Areas.Identity.Data.AdAstraUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>().AddEntityFrameworkStores<AdAstraContext>();

            builder.Services.AddAuthorization(options => options.AddPolicy("AdminRequirement", policy => policy.RequireRole("Admin", "Higgs")));
            builder.Services.AddAuthorization(options => options.AddPolicy("HiggsRequirement", policy => policy.RequireRole("Higgs")));

            builder.Services.AddHttpClient();
            // Add services to the container.
            builder.Services.AddRazorPages();
            //builder.Services.AddRazorPages(options => {
            //    options.Conventions.AuthorizeFolder("/AdminRole", "HiggsRequirement");
            //    options.Conventions.AuthorizeFolder("/Admin/AdminCategory", "AdminRequirement");
            //    options.Conventions.AuthorizeFolder("/Admin/AdminMessage", "AdminRequirement");
            //    options.Conventions.AuthorizeFolder("/Admin/AdminPost", "AdminRequirement");
            //    options.Conventions.AuthorizeFolder("/Admin/AdminReply", "AdminRequirement");
            //    options.Conventions.AuthorizeFolder("/Admin/AdminReport", "AdminRequirement");

            //    // Now accessible by both Admin and Higgs roles
            //});


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
