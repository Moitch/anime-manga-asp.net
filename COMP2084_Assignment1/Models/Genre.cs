using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Assignment1.Models
{
    // Genre is a small table with only an ID and a Name variable.
    // It is used to store all of the possible Genres I would like to have
    // on the site. It is used to give Anime/Manga their corresponding genres.
    public class Genre
    {

        public int ID { get; set; }

        [Required]
        public String Name { get; set; }

        //References
        public List<Manga> Mangas { get; set; }
        public List<Anime> Animes { get; set; }


    }
}
