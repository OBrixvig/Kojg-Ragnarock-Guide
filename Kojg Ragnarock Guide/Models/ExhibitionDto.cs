using System.ComponentModel.DataAnnotations;

namespace Kojg_Ragnarock_Guide.Models
{
    public class ExhibitionDto
    {
        [Required,MaxLength(20)]
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        
        public IFormFile? PhotoFile { get; set; }
        
        //   public IFormFile AudioFile { get; set; } 
        public string Floor { get; set; } = "";
    }
}
