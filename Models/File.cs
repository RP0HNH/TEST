using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CloudFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime UploadDate { get; set; }
        public long Size { get; set; }
        public string Format { get; set; }
    }


}
