using System.ComponentModel.DataAnnotations;

namespace PassionProject.Models
{
    public class AlbumArtist
    {
        [Key]
        public int AlbumArtistId { get; set; }


        public string AlbumArtistName { get; set; }

        public string? AlbumArtistBio { get; set; }

        //Collect Album Ids Associated with Artist
        public virtual ICollection<Album>? Albums { get; set; } = new List<Album>();

    }
}
