using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kojg_Ragnarock_Guide.Pages.Admin
{
    [Authorize(Roles = "admin")]
    public class DeleteExhibitionModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly ExhibitionDbContext context;

        private string audioFullPath;
        private string photoFullPath;

        private Exhibition? exhibition;

        public DeleteExhibitionModel(IWebHostEnvironment environment, ExhibitionDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            ValidateID(id);

            FindExhibition(id);

            DeleteAudio();

            DeletePhoto();

            DeleteExhibition();

            //redirect to Index page
            Response.Redirect("/Admin/AdminEpisodePage");
        }

        private void ValidateID(int? id)
        {
            //This will get the chosen object in Exhibition index and call this code if nothing is found it will return til index page
            if (id == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
                return;
            }
        }

        private void FindExhibition(int? id)
        {
            // looks for exhibition and if nothing comes up return to page.
            exhibition = context.Exhibitions.Find(id);
            if (exhibition == null)
            {
                Response.Redirect("/Admin/AdminEpisodePage");
                return;
            }
        }

        private void DeleteAudio()
        {
            // Deletes Audio
            audioFullPath = environment.WebRootPath + "/exhibitionAudios/" + exhibition.AudioFileName;
            System.IO.File.Delete(audioFullPath);
        }

        private void DeletePhoto()
        {
            // Deletes Photo
            photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + exhibition.PhotoFileName;
            System.IO.File.Delete(photoFullPath);
        }

        private void DeleteExhibition()
        {
            //Deletes the the rest of the object
            context.Exhibitions.Remove(exhibition);
            context.SaveChanges();
        }
    }
}
