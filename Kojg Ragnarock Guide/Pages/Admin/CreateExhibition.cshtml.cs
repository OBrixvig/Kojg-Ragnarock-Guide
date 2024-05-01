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

        public string _errorMassage = "";
        public string _successMassage = "";

        public void OnPost() 
        {
            if (ExhibitionDto.PhotoFile == null)
            {
                ModelState.AddModelError("ExhibitionDto.PhotoFile", "Du er nød til at uploade et billed");
            }
            if (!ModelState.IsValid)
            {
                _errorMassage = "Udfyld venligst alle ledige felter";
                return;
            }

            // save Image as a file
            string newPhotoFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newPhotoFileName += Path.GetExtension(ExhibitionDto.PhotoFile!.FileName);

            string photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + newPhotoFileName;
            using (var stream = System.IO.File.Create(photoFullPath)) 
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
                
            };

            context.Exhibitions.Add(exhibition);
            context.SaveChanges();

            //Clear the form
            ExhibitionDto.Title = "";
            ExhibitionDto.Description = "";
            ExhibitionDto.Floor = "";
            ExhibitionDto.PhotoFile = null;
            

            ModelState.Clear();

            _successMassage = "Udstilling er oprettet";

            Response.Redirect("/Exhibitions/Index");
        }
    }
}
