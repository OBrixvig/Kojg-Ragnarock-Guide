using Kojg_Ragnarock_Guide.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Interfaces;
namespace Kojg_Ragnarock_Guide
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Add SQL DB
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ExhibitionDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQL-Ragnarock-Database") ?? throw new InvalidOperationException("Connection string 'ExhibitionContext' not found.")));

            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ExhibitionDbContext>();
            builder.Services.AddTransient<IExhibitionRepository, ExhibitionRepository>();


            // add blob service Dont think we need it, was ment for audio



            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
