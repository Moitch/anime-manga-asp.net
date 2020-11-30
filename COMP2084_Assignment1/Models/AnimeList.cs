using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Assignment1.Models
{
    // Currently AnimeList is unused but will be used in the future
    // It has an ID, AnimeID, EpisodesWatched, Rating, Status, and a Photo column.
    // Users will put what animes they are watching in this table and will be able
    // track their progress in this table.
    // It uses the Anime table as a parent to grab the animes from.
    public class AnimeList
    {

        public int ID { get; set; }

        [Required]
        public int AnimeID { get; set; }

        public int EpisodesWatched { get; set; }

        public int Rating { get; set; }

        [Required]
        public String Status { get; set; }

        public String Photo { get; set; }

        //Parent
        public Anime Animes { get; set; }

    }
}
