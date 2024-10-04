using System.ComponentModel.DataAnnotations;

namespace PassionProject.Models
{
    public class Color
    {
        [Key]
        public int ColorId { get; set; }
        public string ColorName { get; set; }

        //a color can have many cards (many-to-many)
        public ICollection<Card> Cards { get; set; }
    }

    public class ColorDto
    {
        public string ColorName { get; set; }

        public string CardCount { get; set; }
    }
}
