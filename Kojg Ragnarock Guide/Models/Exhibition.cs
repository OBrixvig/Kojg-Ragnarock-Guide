using System.ComponentModel.DataAnnotations;

namespace Kojg_Ragnarock_Guide.Models
{
    public class Exhibition
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Title { get; set; } = "";   
        public string Description { get; set; } = "";
        [MaxLength(100)]
        public string PhotoFileName { get; set; } = "";
 //       public string AudioFileName { get; set; } = "";

        public string Floor { get; set; } = "";
    }
}   // This defines what an Exhibition should be
