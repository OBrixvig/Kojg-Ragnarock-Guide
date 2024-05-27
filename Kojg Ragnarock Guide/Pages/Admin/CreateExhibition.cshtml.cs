using Azure.Storage.Blobs;
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

        private readonly IWebHostEnvironment environment;
        private readonly ExhibitionDbContext context;

        private string newAudioFileName;
        private string newPhotoFileName;

        private string audioFullPath;
        private string photoFullPath;

        [BindProperty] public ExhibitionDto ExhibitionDto { get; set; } = new ExhibitionDto();

        // making some empty string massages i use in html razer pages lower i define them
        public string _errorMassage = "";
        public string _successMassage = "";

        public CreateExhibitionModel(IWebHostEnvironment environment, ExhibitionDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet()
        {
        }


        public void OnPost()
        {
            ValidateInput();

            SaveAudioAsFile();

            SaveImageAsFile();

            SaveNewExhibitionInDatabase();

            ClearTheForm();

            _successMassage = "Udstilling er oprettet";

            Response.Redirect("/Admin/AdminEpisodePage");
        }


        private void ValidateInput()
        {
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
        }

        private void SaveAudioAsFile()
        {
            // save Audio as a file
            newAudioFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newAudioFileName += Path.GetExtension(ExhibitionDto.AudioFile!.FileName);

            audioFullPath = environment.WebRootPath + "/exhibitionAudios/" + newAudioFileName;
            using (FileStream? stream = System.IO.File.Create(audioFullPath))
            {
                ExhibitionDto.AudioFile.CopyTo(stream);
            }
        }

        private void SaveImageAsFile()
        {
            // save Image as a file
            newPhotoFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newPhotoFileName += Path.GetExtension(ExhibitionDto.PhotoFile!.FileName);

            photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + newPhotoFileName;
            using (FileStream? stream = System.IO.File.Create(photoFullPath))
            {
                ExhibitionDto.PhotoFile.CopyTo(stream);
            }
        }

        private void SaveNewExhibitionInDatabase()
        {
            //save the new product in the database
            Exhibition exhibition = new Exhibition()
            {
                Title = ExhibitionDto.Title,
                ExhibitionNumber = ExhibitionDto.ExhibitionNumber,
                Description = ExhibitionDto.Description ?? "",
                Floor = ExhibitionDto.Floor,
                PhotoFileName = newPhotoFileName,
                AudioFileName = newAudioFileName,
            };
            context.Exhibitions.Add(exhibition);
            context.SaveChanges();
        }

        private void ClearTheForm()
        {
            //Clear the form
            ExhibitionDto.Title = "";
            ExhibitionDto.ExhibitionNumber = 0;
            ExhibitionDto.Description = "";
            ExhibitionDto.Floor = "";
            ExhibitionDto.PhotoFile = null;
            ExhibitionDto.AudioFile = null;

            ModelState.Clear();
        }
    }
}
