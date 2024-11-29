using System.ComponentModel.DataAnnotations;

namespace PassionProject.Models
{
    public class Track
    {
        [Key]
        public int TrackId { get; set; }

        [Required(ErrorMessage = "Track Title Required")]
        public string TrackTitle { get; set; }

        public TimeSpan TrackLength { get; set; }

        /// <summary>
        /// Using {id} to link Albums database with tracks, listing which album the track is associated with
        /// </summary>
        public int AlbumId { get; set; }
    }
}
