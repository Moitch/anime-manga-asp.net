using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Assignment1.Models
{
    // This creates the parameters for the Anime table in the database.
    // It has an ID, GenreID, Name, Episodes, Status, AirStart, AirEnd, and a Studios column.
    // In this class it also tells the database what tables it will reference,
    // Which is Genres as a parent database and AnimeLists as a child.
    public class Anime
    {
        
        public int ID { get; set; }

        [Required]
        public int GenreID { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public int Episodes { get; set; }

        [Required]
        [Display(Name = "Ongoing")]
        public Boolean Status { get; set; }

        [Required]
        [Display(Name = "Airing Started")]
        public DateTime AirStart { get; set; }

        [Display(Name = "Airing Ended")]
        public DateTime AirEnd { get; set; }

        public String Studios { get; set; }

        //Parent database reference
        public Genre Genres { get; set; }

        //Child database reference
        public List<AnimeList> AnimeLists { get; set; }
    }
}
