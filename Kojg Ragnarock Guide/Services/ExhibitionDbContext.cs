    using Kojg_Ragnarock_Guide.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Kojg_Ragnarock_Guide.Services
{
    public class ExhibitionDbContext : IdentityDbContext<AppUser>
    {
        public ExhibitionDbContext(DbContextOptions options) : base(options)
        {
            
        }
        //What we want to make a Database
        //In Package Manager Console Write Add-Migration and what you want the name to be i named et after the databasefollowed by Migration
        //After that Update the datebase by writing Update-Database in PMC
        public DbSet<Exhibition> Exhibitions { get; set; }

        //Creating diffrent roles that we can use with auth.
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var admin = new IdentityRole("admin");
            admin.NormalizedName = "admin";

            var client = new IdentityRole("client");
            client.NormalizedName = "client";

            builder.Entity<IdentityRole>().HasData(admin, client);

        }
    }
}
