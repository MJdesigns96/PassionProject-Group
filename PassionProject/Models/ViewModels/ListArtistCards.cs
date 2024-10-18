namespace PassionProject.Models.ViewModels
{
    public class ListArtistCards
    {
        public Artist ArtistName { get; set; }

        public IEnumerable<Card> Card { get; set; }
    }
}
