using Azure.Storage.Blobs;
using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kojg_Ragnarock_Guide.Pages.Admin
{
    public class CreateExhibitionModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ExhibitionDbContext context;

        [BindProperty]
        public ExhibitionDto ExhibitionDto { get; set; } = new ExhibitionDto();

        public CreateExhibitionModel(IWebHostEnvironment environment, ExhibitionDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet()
        {
        }
        // making some empty string massages i use in html razer pages lower i define them
        public string _errorMassage = "";
        public string _successMassage = "";
        
        public void OnPost() 
        {   // Code below calls some errors if photo or alle the reqired information is not fill out in form
            if (ExhibitionDto.PhotoFile == null)
            {
                ModelState.AddModelError("ExhibitionDto.PhotoFile", "Du er nød til at uploade et billed");
            }
            if (ExhibitionDto.AudioFile == null)
            {
                ModelState.AddModelError("ExhibitionDto.AudioFile", "Du er nød til at uploade mp3 lydfil");
            }
            if (!ModelState.IsValid)
            {
                _errorMassage = "Udfyld venligst alle ledige felter";
                return;
            }

            // save Audio as a file
            string newAudioFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newAudioFileName += Path.GetExtension(ExhibitionDto.AudioFile!.FileName);

            string audioFullPath = environment.WebRootPath + "/exhibitionPhotos/" + newAudioFileName;
            using (FileStream? stream = System.IO.File.Create(audioFullPath))
            {
                ExhibitionDto.AudioFile.CopyTo(stream);
            }

            // save Image as a file
            string newPhotoFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newPhotoFileName += Path.GetExtension(ExhibitionDto.PhotoFile!.FileName);

            string photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + newPhotoFileName;
            using (FileStream? stream = System.IO.File.Create(photoFullPath)) 
            {
                ExhibitionDto.PhotoFile.CopyTo(stream);
            }

            //save the new product in the datebase
            Exhibition exhibition = new Exhibition()
            {
                Title = ExhibitionDto.Title,
                Description = ExhibitionDto.Description ?? "",
                Floor = ExhibitionDto.Floor,
                PhotoFileName =newPhotoFileName,
                AudioFileName =newAudioFileName,
            };
            context.Exhibitions.Add(exhibition);
            context.SaveChanges();

            //Clear the form
            ExhibitionDto.Title = "";
            ExhibitionDto.Description = "";
            ExhibitionDto.Floor = "";
            ExhibitionDto.PhotoFile = null;
            ExhibitionDto.AudioFile = null;

            ModelState.Clear();

            _successMassage = "Udstilling er oprettet";

            Response.Redirect("/Exhibitions/Index");
        }
    }
}
