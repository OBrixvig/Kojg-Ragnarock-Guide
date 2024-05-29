using Kojg_Ragnarock_Guide.Interfaces;
using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kojg_Ragnarock_Guide.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class DeleteExhibitionModel : PageModel
    {
        IExhibitionRepository repo;

        public DeleteExhibitionModel(IExhibitionRepository repository)
        {
            repo = repository;
        }
        public void OnGet(int? id)
        {
            //Validate ID
            if (id == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
                return;
            }

            //Delete Audio
            repo.DeleteAudio(id);

            //Delete Photo
            repo.DeletePhoto(id);

            //Delete Exhibition
            repo.DeleteExhibition(id);

            //redirect to Index page
            Response.Redirect("/Admin/AdminEpisodePage");
        }
    }
}
