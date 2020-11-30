using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Assignment1.Models
{
    // This creates the parameters for the Anime table in the database.
    // It has an ID, GenreID, Name, Volumes, Chapters, Status, PublishStart, PublishEnd, and an Authors column.
    // In this class it also tells the database what tables it will reference,
    // Which is Genres as a parent database and MangaLists as a child.
    public class Manga
    {
        public int ID { get; set; }

        [Required]
        public int GenreID { get; set; }

        [Required]
        public String Name { get; set; }

        
        public int Volumes { get; set; }

        
        public int Chapters { get; set; }

        [Required]
        public Boolean Status { get; set; }

        [Required]
        [Display(Name = "Publishing Start")]
        public DateTime PublishStart { get; set; }

        [Display(Name = "Publishing End")]
        public DateTime PublishEnd { get; set; }

        public String Authors { get; set; }

        //Parent database reference
        public Genre Genres { get; set; }

        //Child database reference
        public List<MangaList> MangaLists { get; set; }
    }
}
