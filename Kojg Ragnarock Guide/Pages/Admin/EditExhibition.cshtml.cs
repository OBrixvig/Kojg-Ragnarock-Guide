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

        private Exhibition? exhibition;

        private string newPhotoFileName;
        private string photoFullPath;
        private string oldPhotoFullPath;

        private string newAudioFileName;
        private string audioFullPath;
        private string oldAudioFullPath;

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
            ValidateID(id);

            FindExhibitionInDatabase(id);

            if (exhibition == null)
            {
                _errorMessage = "Exhibition not found.";
                return;
            }

            CopyExhibition();

            DisplayExhibiion();

        }

        public void OnPost(int? id)
        {
            ValidateID(id);

            FindExhibitionInDatabase(id);

            if (exhibition == null)
            {
                _errorMessage = "Exhibition not found.";
                Response.Redirect("/Admin/AdminEpisodePage");
            }

            ValidateModelState();

            UpdateAudio();

            UpdatePhoto();

            UpdateExhibitionInDatabase();

            DisplayExhibiion();

            _successMessage = "Udstilling blev opdateret";

            Response.Redirect("/Admin/AdminEpisodePage");

        }

        private void ValidateID(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
            }
        }

        private void FindExhibitionInDatabase(int? id)
        {
            if (id.HasValue)
            {
                exhibition = context.Exhibitions.Find(id.Value);
                if (exhibition == null)
                {
                    Response.Redirect("/Admin/AdminEpisodePage");
                }
            }
            else
            {
                Response.Redirect("/Admin/AdminEpisodePage");
            }
        }

        private void CopyExhibition()
        {
            // Return what I want to update
            if (exhibition != null)
            {
                ExhibitionDto.Title = exhibition.Title;
                ExhibitionDto.Description = exhibition.Description;
                ExhibitionDto.Floor = exhibition.Floor;
                ExhibitionDto.ExhibitionNumber = exhibition.ExhibitionNumber;
            }
        }

        private void ValidateModelState()
        {
            // If ModelState is not valid, return error massage
            if (!ModelState.IsValid)
            {
                _errorMessage = "Udfyld alle felter";
                return;
            }
        }

        private void UpdateAudio()
        {
            // Update Audio If we have a new one
            newAudioFileName = exhibition.AudioFileName;
            if (ExhibitionDto.AudioFile != null)
            {
                // Important to get Timestamp or else it wont save the picture properly
                newAudioFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newAudioFileName += Path.GetExtension(ExhibitionDto.AudioFile!.FileName);
                // Saves the new audio chosen
                audioFullPath = environment.WebRootPath + "/exhibitionAudios/" + newAudioFileName;
                using (FileStream? stream = System.IO.File.Create(audioFullPath))
                {
                    ExhibitionDto.AudioFile.CopyTo(stream);
                }

                // delete old audio
                oldAudioFullPath = environment.WebRootPath + "/exhibitionAudios/" + exhibition.AudioFileName;
                System.IO.File.Delete(oldAudioFullPath);
            }
            exhibition.AudioFileName = newAudioFileName;
        }

        private void UpdatePhoto()
        {
            // Update Photo If we have a new one
            newPhotoFileName = exhibition.PhotoFileName;
            if (ExhibitionDto.PhotoFile != null)
            {
                // Important to get Timestamp or else it wont save the picture properly
                newPhotoFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newPhotoFileName += Path.GetExtension(ExhibitionDto.PhotoFile!.FileName);
                // Saves the new picture chosen
                photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + newPhotoFileName;
                using (FileStream? stream = System.IO.File.Create(photoFullPath))
                {
                    ExhibitionDto.PhotoFile.CopyTo(stream);
                }
                // delete old photo
                oldPhotoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + exhibition.PhotoFileName;
                System.IO.File.Delete(oldPhotoFullPath);
            }
            exhibition.PhotoFileName = newPhotoFileName;
        }

        private void UpdateExhibitionInDatabase()
        {
            // update exhibition in database
            exhibition.Title = ExhibitionDto.Title;
            exhibition.ExhibitionNumber = ExhibitionDto.ExhibitionNumber;
            exhibition.Description = ExhibitionDto.Description ?? "";
            exhibition.Floor = ExhibitionDto.Floor;
            context.SaveChanges();
        }

        private void DisplayExhibiion()
        {
            Exhibition = exhibition;
        }


    }
}
