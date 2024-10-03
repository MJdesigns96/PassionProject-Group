using System.ComponentModel.DataAnnotations;

namespace PassionProject.Models
{
    public class Artist
    {
        [Key]
        public int ArtistId { get; set; }
        public string ArtistName { get; set; }

        // an artist has many cards (one-to-many)
        public ICollection<Card>? Cards { get; set; }
    }
}
