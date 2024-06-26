using Kojg_Ragnarock_Guide.Interfaces;
using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kojg_Ragnarock_Guide.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class EditExhibitionModel : PageModel
    {

        private IExhibitionRepository repo;

        [BindProperty]
        public ExhibitionDto ExhibitionDto { get; set; } = new ExhibitionDto();

        public Exhibition Exhibition { get; set; } = new Exhibition();

        public string ErrorMessage { get; private set; } = "";
        public string SuccessMessage { get; private set; } = "";

        public EditExhibitionModel(IExhibitionRepository repository)
        {
            repo = repository;
        }
        //Making some empty string messages i use in html razer pages, lower i define them
        public void OnGet(int? id)
        {
            //Validate ID
            if (id == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
                return;
            }

            //Find Exhibition
            repo.FindExhibition(id);
            if (repo.FoundExhibition == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
            }

            if (repo.FoundExhibition == null)
            {
                ErrorMessage = "Exhibition not found.";
                return;
            }

            //Copy Exhibition
            repo.CopyFoundExhibition(ExhibitionDto);

            //Display Exhibition
            Exhibition = repo.FoundExhibition;
        }

        public void OnPost(int? id)
        {
            //Validate ID
            if (id == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
                return;
            }

            //Find Exhibition
            repo.FindExhibition(id.Value);
            if (repo.FoundExhibition == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
            }

            //Validate Model State
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Udfyld alle felter";
                return;
            }

            //Update Audio
            repo.UpdateAudio(ExhibitionDto);

            //Update Photo
            repo.UpdatePhoto(ExhibitionDto);

            //Update Exhibition in database
            repo.UpdateExhibition(ExhibitionDto);

            //Display Exhibition
            Exhibition = repo.FoundExhibition;


            SuccessMessage = "Udstilling blev opdateret";

            Response.Redirect("/Admin/AdminEpisodePage");

        }
    }
}
