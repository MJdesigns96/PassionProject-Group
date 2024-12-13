namespace PassionProject.Models.ViewModels
{
    public class ArtistAdmin
    {
        // This ViewModel is the structure needed for us to render
        // ArtistsPage/List.cshtml

        public IEnumerable<ArtistDto> Artists { get; set; }
        public bool isAdmin { get; set; }
    }
}
