using Azure.Storage.Blobs;
using Kojg_Ragnarock_Guide.Interfaces;
using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kojg_Ragnarock_Guide.Pages.Admin
{

    [Authorize(Roles = "admin")]
    public class CreateExhibitionModel : PageModel
    {
        private IExhibitionRepository repo;

        [BindProperty]
        public ExhibitionDto ExhibitionDto { get; set; } = new ExhibitionDto();

        //Making some empty string messages i use in html razer pages, lower i define them
        public string ErrorMessage { get; private set; } = "";
        public string SuccessMessage { get; private set; } = "";

        public CreateExhibitionModel(IExhibitionRepository repository)
        {
            repo = repository;
        }

        public void OnGet()
        {
        }


        public void OnPost()
        {
            //Validate Input
            if (ExhibitionDto.PhotoFile == null)
            {
                ModelState.AddModelError("ExhibitionDto.PhotoFile", "Du er nødt til at uploade et billed");
            }

            else if (ExhibitionDto.AudioFile == null)
            {
                ModelState.AddModelError("ExhibitionDto.AudioFile", "Du er nødt til at uploade mp3 lydfil");
            }

            else if (!ModelState.IsValid)
            {
                ErrorMessage = "Udfyld venligst alle ledige felter";
                return;
            }
            else
            {
                //Save Audio
                repo.SaveAudioAsFile(ExhibitionDto);

                //Save Photo
                repo.SavePhotoAsFile(ExhibitionDto);

                //Save Exhibition
                repo.CreateExhibition(ExhibitionDto);

                //Clear Form
                repo.ClearTheForm(ExhibitionDto);

                ModelState.Clear();

                SuccessMessage = "Udstilling er nu oprettet";

                Response.Redirect("/Admin/AdminEpisodePage");
            }
        }
    }
}
