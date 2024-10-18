using PassionProject.Models;

namespace PassionProject.Models.ViewModels
{
    public class CardNew
    {
        public string CardName { get; set; }
        public string Colours { get; set; }
        public string Artist { get; set; }
        public int ArtistId { get; set; }

        // package the artist and color data for the new card
        public virtual Artist ArtistName { get; set; }
        public IEnumerable<Artist> artists { get; set; }

        public int ColorId { get; set; }
        public IEnumerable<ColorDto> colors { get; set; }
    }
}
