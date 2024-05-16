using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Kojg_Ragnarock_Guide.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class AdminEpisodePage : PageModel
    {
        private readonly ExhibitionDbContext context;

        public List<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public AdminEpisodePage(ExhibitionDbContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
            Exhibitions = context.Exhibitions.OrderByDescending(E => E.ExhibitionNumber).Reverse().ToList();
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                Exhibitions = FilterExhibitions(FilterCriteria);
            }
        }

        public List<Exhibition> FilterExhibitions(string etageNr)
        {
            List<Exhibition> filteredList = new List<Exhibition>();

            foreach (Exhibition ex in Exhibitions)
            {
                if (ex.Floor.Contains(etageNr))
                {
                    filteredList.Add(ex);
                }
            }
            return filteredList;
        }
    }
}
