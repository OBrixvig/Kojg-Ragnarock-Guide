using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kojg_Ragnarock_Guide.Pages.Exhibitions
{
    public class AdminEpisodePage : PageModel
    {
        private readonly ExhibitionDbContext context;
        
        public List<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();

        public AdminEpisodePage(ExhibitionDbContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
            Exhibitions = context.Exhibitions.OrderByDescending(E => E.Id).ToList();
        }
    }
}
