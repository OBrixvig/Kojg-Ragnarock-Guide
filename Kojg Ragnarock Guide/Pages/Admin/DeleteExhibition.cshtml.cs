using Kojg_Ragnarock_Guide.Models;
using Kojg_Ragnarock_Guide.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Kojg_Ragnarock_Guide.Pages.Admin
{
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
            if (id == null)
            {
                Response.Redirect("/Exhibitions/Index");
                return;
            }

          var exhibidition = context.Exhibitions.Find(id);
            if (exhibidition == null)
            {
                Response.Redirect("/Exhibitions/Index");
                return;
            }
            // Deletes Photo
            string imageFullPath = environment.WebRootPath + "/exhibitionPhotos/" + exhibidition.PhotoFileName;
            System.IO.File.Delete(imageFullPath);
            //Deletes the the rest of the objekt
            context.Exhibitions.Remove(exhibidition);
            context.SaveChanges();

            Response.Redirect("/Exhibitions/Index");
        }
    }
}
