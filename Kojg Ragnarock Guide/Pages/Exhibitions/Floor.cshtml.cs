using Kojg_Ragnarock_Guide.Interfaces;
using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Core.Types;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kojg_Ragnarock_Guide.Pages.Exhibitions
{
    [Authorize(Roles = "admin,client")]
    public class Floor : PageModel
    {
        private IExhibitionRepository repo;

        public List<Exhibition> Exhibitions { get; private set; } = new List<Exhibition>();

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public Floor(IExhibitionRepository repository)
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
