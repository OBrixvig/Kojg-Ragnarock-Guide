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
        public ExhibitionDto exhibitionDto { get; set; } = new ExhibitionDto();

        public CreateExhibitionModel(IWebHostEnvironment environment, ExhibitionDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }

        public void OnGet()
        {
        }

        public string _errorMassage = "";
        public string _successMassage = "";

        public void OnPost() 
        {
            if (exhibitionDto.PhotoFile == null)
            {
                ModelState.AddModelError("exhibitionDto.PhotoUrl", "Du er nød til at linke et billed");
            }
            if (!ModelState.IsValid)
            {
                _errorMassage = "Udfyld venligst alle ledige felter";
                return;
            }

            //save Audio file
            string newAudioFileName = Path.GetExtension(exhibitionDto.AudioFile!.FileName);

            string AudioFullPath = environment.WebRootPath + "/exhibitions/" + newAudioFileName;
            using (System.IO.FileStream stream = System.IO.File.Create(AudioFullPath))
            {
                exhibitionDto.AudioFile.CopyTo(stream);
            }

            //save Image as a file
            string newPhotoFileName = Path.GetExtension(exhibitionDto.PhotoFile!.FileName);

            string PhotoFullPath = environment.WebRootPath + "/exhibitions/" + newPhotoFileName;
            using (System.IO.FileStream stream = System.IO.File.Create(PhotoFullPath)) 
            {
                exhibitionDto.PhotoFile.CopyTo(stream);
            }

            //save the new product in the datebase
            Exhibition exhibition = new Exhibition()
            {
                Title = exhibitionDto.Title,
                Description = exhibitionDto.Description ?? "",
                PhotoFile =newPhotoFileName,
                AudioFile =newAudioFileName,
            };

            context.Exhibitions.Add(exhibition);
            context.SaveChanges();

            //Clear the form
            exhibitionDto.Title = "";
            exhibitionDto.Description = "";
            exhibitionDto.AudioFile = null;
            exhibitionDto.PhotoFile = null;
            
            ModelState.Clear();

            _successMassage = "Udstilling er oprettet";

            Response.Redirect("/Exhibitions/Index");
        }
    }
}
