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

        public DeleteExhibitionModel(IWebHostEnvironment environment, ExhibitionDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            //This get the the chosen objekt in Exhibition index and call this code if nothing is found it will return til index page
            if (id == null)
            {
                Response.Redirect("/Exhibitions/AdminEpisodePage");
                return;
            }
            // looks for exhibition and if nothing comes up return to page.
            Exhibition? exhibidition = context.Exhibitions.Find(id);
            if (exhibidition == null)
            {
                Response.Redirect("/Exhibitions/AdminEpisodePage");
                return;
            }

            // Deletes Audio
            string audioFullPath = environment.WebRootPath + "/exhibitionAudios/" + exhibidition.AudioFileName;
            System.IO.File.Delete(audioFullPath);

            // Deletes Photo
            string photoFullPath = environment.WebRootPath + "/exhibitionPhotos/" + exhibidition.PhotoFileName;
            System.IO.File.Delete(photoFullPath);

            //Deletes the the rest of the objekt
            context.Exhibitions.Remove(exhibidition);
            context.SaveChanges();
            //redirect to Index page
            Response.Redirect("/Exhibitions/AdminEpisodePage");
        }
    }
}
