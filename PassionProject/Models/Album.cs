using System.ComponentModel.DataAnnotations;

namespace PassionProject.Models
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "Album title is required.")]
        public string AlbumTitle { get; set; }

        public string Genre { get; set; }

        [Required(ErrorMessage = "Release Date is required.")]
        public DateOnly ReleaseDate { get; set; }

        /// <summary>
        /// Foreign Key to call Artist via {id}
        /// Collect tracks that link to albums database via {id}
        /// </summary>

        [Required(ErrorMessage = "Artist is required")]
        public int AlbumArtistId { get; set; }

        //public virtual Artist Artist { get; set; }

        public virtual ICollection<Track> Tracks { get; set; } = new List<Track>();
    }
}
