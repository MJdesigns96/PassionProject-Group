﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProject.Models

{
    public class Card
    {
        //each card has a Primary Key
        [Key]
        public int CardId { get; set; }
        public string CardName { get; set; }
        public string Colours { get; set; }
        public string Artist { get; set; }

        //a card has one artist (one-to-one)
        public int ArtistId { get; set; }
        public virtual Artist ArtistName { get; set; }

        //a card can have many colors (one-to-many)
        public int ColorId { get; set; }
        public ICollection<Color> Colors { get; set; }

        public static implicit operator Card(ServiceResponse v)
        {
            throw new NotImplementedException();
        }
    }
}
