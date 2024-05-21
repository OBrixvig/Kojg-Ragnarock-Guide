using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kojg_Ragnarock_Guide.Pages.Exhibitions
{
    [Authorize(Roles = "admin,client")]
    public class Floor2 : PageModel
    {
        private readonly ExhibitionDbContext context;

        public List<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();

        public Floor2(ExhibitionDbContext context)
        {
            this.context = context;
        }

        public void OnGet()
        {
            Exhibitions = context.Exhibitions.OrderByDescending(E => E.ExhibitionNumber).Reverse().ToList();
        }

    }
}
