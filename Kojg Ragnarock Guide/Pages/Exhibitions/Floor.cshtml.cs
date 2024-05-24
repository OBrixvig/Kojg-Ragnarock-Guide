using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kojg_Ragnarock_Guide.Pages.Exhibitions
{
    [Authorize(Roles = "admin,client")]
    public class Floor : PageModel
    {
        private readonly ExhibitionDbContext context;

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }


        public List<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();

        public Floor(ExhibitionDbContext context)
        {
            this.context = context;
        }
        public List<Exhibition> ExhibitionFloor { get; set; } = new List<Exhibition>();

        public void OnGet()
        {
           Exhibitions = context.Exhibitions.OrderByDescending(E => E.ExhibitionNumber).Reverse().ToList();
            
            if (!string.IsNullOrEmpty(FilterCriteria))
            {
                Exhibitions = FilterExhibitions(FilterCriteria);
            }

        }
        public List<Exhibition> FilterExhibitions(string FloorNumber)
        {
            List<Exhibition> filteredList = new List<Exhibition>();

            foreach (Exhibition ex in Exhibitions)
            {
                if (ex.Floor.Contains(FloorNumber))
                {
                    filteredList.Add(ex);
                }
            }
            return filteredList;
        }



    }
}
