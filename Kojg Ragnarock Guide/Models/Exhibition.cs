using System.ComponentModel.DataAnnotations;

namespace Kojg_Ragnarock_Guide.Models
{
    public class Exhibition
    {
        public int Id { get; set; }
        public int ExhibitionId { get; set; }
        [MaxLength(14)]
        public string Title { get; set; } = "";   
        public string Description { get; set; } = "";
        public string PhotoFile { get; set; } = "";
        public string AudioFile { get; set; } = "";

        public string Floor { get; set; } = "";
    }
}
