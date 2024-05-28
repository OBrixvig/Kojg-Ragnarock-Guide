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
        IAdminActionsRepository repo;
        public Exhibition Exhibition { get; set; } = new Exhibition();

        public DeleteExhibitionModel(IAdminActionsRepository repository)
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

            //Find 
            repo.FindExhibitionInDatabase(id.Value);
            if (repo.foundExhibition == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
            }

            //Delete Audio
            repo.DeleteAudio();

            //Delete Photo
            repo.DeletePhoto();

            //Delete Exhibition
            repo.DeleteExhibition();

            //redirect to Index page
            Response.Redirect("/Admin/AdminEpisodePage");
        }
    }
}
