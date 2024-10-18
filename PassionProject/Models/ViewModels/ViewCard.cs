namespace PassionProject.Models.ViewModels
{
    public class ViewCard
    {
        //card must have a name
        public string CardName { get; set; }
        //card must have colors
        // card must have an artist
        // package the artist and color data for the card

        public Artist Artist { get; set; }

        public ColorDto Color { get; set; }
    }
}
