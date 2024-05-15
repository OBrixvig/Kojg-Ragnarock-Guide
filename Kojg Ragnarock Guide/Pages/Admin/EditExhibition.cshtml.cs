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
        // the OnGet() searches for the Id and Exhibition and return with the think i want to update
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
                return;
            }

            Exhibition? exhibition = context.Exhibitions.Find(id);
            if (exhibition == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
                return;
            }
            //return what i want to update
            ExhibitionDto.Title = exhibition.Title;
            ExhibitionDto.Description = exhibition.Description;
            ExhibitionDto.Floor = exhibition.Floor;

            Exhibition = exhibition;
        }

        public void OnPost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
                return;
            }
            // If ModelState is not valid, return error massage
            if (!ModelState.IsValid)
            {
                _errorMessage = "Udfyld alle felter";
                return;
            }
            // finds the the exhibition in the database 
            Exhibition? exhibition = context.Exhibitions.Find(id);
            if(exhibition == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
                return;
            }

            // Update Audio If we have a new one
            string newAudioFileName = exhibition.AudioFileName;
            if (ExhibitionDto.AudioFile != null)
            {
                // Important to get Timestamp or else it wont save the picture properly
                newAudioFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newAudioFileName += Path.GetExtension(ExhibitionDto.AudioFile!.FileName);
                // Saves the new audio chosen
                string audioFullPath = environment.WebRootPath + "/exhibitionAudios/" + newAudioFileName;
                using (FileStream? stream = System.IO.File.Create(audioFullPath))
                {
                    ExhibitionDto.AudioFile.CopyTo(stream);
                }
                // delete old audio
                string oldAudioFullPath = environment.WebRootPath + "/exhibitionAudios/" + exhibition.AudioFileName;
                System.IO.File.Delete(oldAudioFullPath);
            }

            // Update Photo If we have a new one
            string newPhotoFileName = exhibition.PhotoFileName;
            if (ExhibitionDto.PhotoFile != null)
            {
                // Important to get Timestamp or else it wont save the picture properly
                newPhotoFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newPhotoFileName += Path.GetExtension(ExhibitionDto.PhotoFile!.FileName);
                // Saves the new picture chosen
                string photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + newPhotoFileName;
                using (FileStream? stream = System.IO.File.Create(photoFullPath))
                {
                    ExhibitionDto.PhotoFile.CopyTo(stream);
                }
                // delete old photo
                string oldPhotoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + exhibition.PhotoFileName;
                System.IO.File.Delete(oldPhotoFullPath);
            }
            // update exhibition in database
            exhibition.Title = ExhibitionDto.Title;
            exhibition.ExhibitionNumber = ExhibitionDto.ExhibitionNumber;
            exhibition.Description = ExhibitionDto.Description ?? "";
            exhibition.Floor = ExhibitionDto.Floor;
            exhibition.PhotoFileName = newPhotoFileName;
            exhibition.AudioFileName = newAudioFileName;

            Exhibition = exhibition;

            _successMessage = "Udstilling blev opdateret";

            context.SaveChanges();

            Response.Redirect("/Admin/AdminEpisodePage");
            
        }
    }
}
