using System.ComponentModel.DataAnnotations.Schema;

namespace projet_oc_fougasse_lover.Models
{
    public class Fougasses
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required float Price { get; set; }
        public required string Adress { get; set; }

        public string? Description { get; set; }
        public required string Text { get; set; }
        
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public string? ImageUrl { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
