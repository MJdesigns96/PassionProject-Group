using System.ComponentModel.DataAnnotations;

namespace PassionProject.Models
{
    public class CardAlbum
    {
        [Key]
        public int Id { get; set; }

        // Foreign Key to Card
        public int CardId { get; set; }
        public virtual Card Card { get; set; }

        // Foreign key to Album
        public int AlbumId { get; set; }
        public virtual Album Album { get; set; }

        // Youtube URL
        public string? YoutubeEmbedUrl { get; set; }
    }
}
