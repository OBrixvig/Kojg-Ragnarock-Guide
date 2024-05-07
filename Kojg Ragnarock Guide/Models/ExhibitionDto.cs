using System.ComponentModel.DataAnnotations;

namespace Kojg_Ragnarock_Guide.Models
{
    public class ExhibitionDto
    {
        [Required,MaxLength(20)]
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        
        public IFormFile? PhotoFile { get; set; }
        
        public IFormFile? AudioFile { get; set; } 

        public string Floor { get; set; } = "";
    }
}
// for the boys
/*data transfer object (DTO) is an object that carries data between processes. 
 * You can use this technique to facilitate communication between two systems (like an API and your server) 
 * without potentially exposing sensitive information. 
 * DTOs are commonsense solutions for people with programming backgrounds*/
