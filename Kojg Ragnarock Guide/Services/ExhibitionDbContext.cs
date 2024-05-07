using Kojg_Ragnarock_Guide.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Kojg_Ragnarock_Guide.Services
{
    public class ExhibitionDbContext :  IdentityDbContext
    {
        public ExhibitionDbContext(DbContextOptions options) : base(options)
        {
            
        }
        //What we want to make a Database
        //In Package Manager Console Write Add-Migration and what you want the name to be i named et after the databasefollowed by Migration
        //After that Update the datebase by writing Update-Database in PMC
        public DbSet<Exhibition> Exhibitions { get; set; }
    }
}
