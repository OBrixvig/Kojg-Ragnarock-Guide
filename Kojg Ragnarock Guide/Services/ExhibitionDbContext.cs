using Kojg_Ragnarock_Guide.Models;
using Microsoft.EntityFrameworkCore;

namespace Kojg_Ragnarock_Guide.Services
{
    public class ExhibitionDbContext : DbContext 
    {
        public ExhibitionDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Exhibition> Exhibitions { get; set; }
    }
}
