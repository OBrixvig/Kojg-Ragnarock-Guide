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

        IExhibitionRepository repo;

        [BindProperty]
        public ExhibitionDto ExhibitionDto { get; set; } = new ExhibitionDto();

        public Exhibition Exhibition { get; set; } = new Exhibition();


        public string _errorMessage = "";
        public string _successMessage = "";

        public EditExhibitionModel(IExhibitionRepository repository)
        {
            repo = repository;
        }
        // the OnGet() searches for the Id and Exhibition and return with the think i want to update
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
            if (repo.foundExhibition == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
            }

            if (repo.foundExhibition == null)
            {
                _errorMessage = "Exhibition not found.";
                return;
            }

            //Copy Exhibition
            repo.CopyFoundExhibition(ExhibitionDto);

            //Display Exhibition
            Exhibition = repo.foundExhibition;
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
            if (repo.foundExhibition == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
            }

            //Validate Model State
            if (!ModelState.IsValid)
            {
                _errorMessage = "Udfyld alle felter";
                return;
            }

            //Update Audio
            repo.UpdateAudio(ExhibitionDto);

            //Update Photo
            repo.UpdatePhoto(ExhibitionDto);

            //Update Exhibition in database
            repo.UpdateExhibition(ExhibitionDto);

            //Display Exhibition
            Exhibition = repo.foundExhibition;


            _successMessage = "Udstilling blev opdateret";

            Response.Redirect("/Admin/AdminEpisodePage");

        }
    }
}
