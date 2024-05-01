using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kojg_Ragnarock_Guide.Pages.Admin
{
    public class EditExhibitionModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ExhibitionDbContext context;

        [BindProperty]
        public ExhibitionDto ExhibitionDto { get; set; } = new ExhibitionDto();

        public Exhibition Exhibition { get; set; } = new Exhibition();

        public string _errorMessage = "";
        public string _successMessage = "";

        public EditExhibitionModel(IWebHostEnvironment environment, ExhibitionDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Exhibitions/Index");
                return;
            }

            Exhibition? exhibition = context.Exhibitions.Find(id);
            if (exhibition == null)
            {
                Response.Redirect("/Exhibitions/Index");
                return;
            }

            ExhibitionDto.Title = exhibition.Title;
            ExhibitionDto.Description = exhibition.Description;
            ExhibitionDto.Floor = exhibition.Floor;

            Exhibition = exhibition;
        }

        public void onPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Exhibitions/Index");
                return;
            }
            if (!ModelState.IsValid)
            {
                _errorMessage = "Udfyld alle felter";
                return;
            }

            var exhibition = context.Exhibitions.Find(id);
            if(exhibition == null)
            {
                Response.Redirect("/Exhibitions/Index");
                return;
            }

            // Update Photo If we have a new one
            string newPhotoFileName = exhibition.PhotoFileName;
            if (ExhibitionDto.PhotoFile != null)
            {
                // Important to get Timestamp or else it wont save the picture proper. dunno why
                newPhotoFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newPhotoFileName += Path.GetExtension(ExhibitionDto.PhotoFile!.FileName);

                string photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + newPhotoFileName;
                using (var stream = System.IO.File.Create(photoFullPath))
                {
                    ExhibitionDto.PhotoFile.CopyTo(stream);
                }
                
                // delete old photo
                string oldPhotoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + exhibition.PhotoFileName;
                System.IO.File.Delete(oldPhotoFullPath);
            }
            // update exhibition in database
            exhibition.Title = ExhibitionDto.Title;
            exhibition.Description = ExhibitionDto.Description ?? "";
            exhibition.Floor = ExhibitionDto.Floor;
            exhibition.PhotoFileName = newPhotoFileName;

            Exhibition = exhibition;

            context.SaveChanges();

            
            _successMessage = "Udstilling blev opdateret";

            Response.Redirect("/Exhibitions/Index");
            
        }
    }
}
