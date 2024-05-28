using Kojg_Ragnarock_Guide.Interfaces;
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
        IExhibitionRepository repo;

        public List<Exhibition> Exhibitions { get; private set; } = new List<Exhibition>();

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public AdminEpisodePage(IExhibitionRepository repository)
        {
            repo = repository;
        }
        public void OnGet()
        {
            Exhibitions = repo.GetAllExhibitions();
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                Exhibitions = repo.FilterExhibitions(FilterCriteria);
            }
        }
    }
}
