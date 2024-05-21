using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication13.Models
{
    public class Teams
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
       
        public string Job { get; set; }

        public string? ImgUrl { get; set; }
        [NotMapped]
        public IFormFile ImgFile { get; set; }



    }
}
