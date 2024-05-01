using System.ComponentModel.DataAnnotations;

namespace Kojg_Ragnarock_Guide.Models
{
    public class ExhibitionDto
    {
        public int Id { get; set; }
        public int ExhibitionId { get; set; }
        [Required,MaxLength(14)]
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        [Required, MaxLength(100)]
        public IFormFile PhotoFile { get; set; } 
        [Required, MaxLength(100)]
        public IFormFile AudioFile { get; set; } 
        public string Floor {  get; set; }
    }
}
