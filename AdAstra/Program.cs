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

            builder.Services.AddDefaultIdentity<Areas.Identity.Data.AdAstraUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AdAstraContext>();

            // Add services to the container.
            builder.Services.AddRazorPages();

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
