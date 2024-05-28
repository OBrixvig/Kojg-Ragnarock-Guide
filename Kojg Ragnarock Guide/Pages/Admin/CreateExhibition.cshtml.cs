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
        IExhibitionRepository repo;

        [BindProperty]
        public ExhibitionDto ExhibitionDto { get; set; } = new ExhibitionDto();

        // making some empty string massages i use in html razer pages lower i define them
        public string _errorMassage = "";
        public string _successMassage = "";

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

            if (ExhibitionDto.AudioFile == null)
            {
                ModelState.AddModelError("ExhibitionDto.AudioFile", "Du er nødt til at uploade mp3 lydfil");
            }

            if (!ModelState.IsValid)
            {
                _errorMassage = "Udfyld venligst alle ledige felter";
                return;
            }

            //Save Audio
            repo.SaveAudioAsFile(ExhibitionDto);

            //Save Image
            repo.SavePhotoAsFile(ExhibitionDto);

            //Save Exhibition
            repo.CreateExhibition(ExhibitionDto);

            //Clear Form
            repo.ClearTheForm(ExhibitionDto);

            ModelState.Clear();

            _successMassage = "Udstilling er oprettet";

            Response.Redirect("/Admin/AdminEpisodePage");
        }


       
    }
}
