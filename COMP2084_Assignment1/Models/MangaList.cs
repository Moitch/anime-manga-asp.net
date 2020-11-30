using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084_Assignment1.Models
{

    // This is the MangaList table
    // It holds all the information the user would want to track when they are
    // Actively reading a manga. It has an ID, MangaID, VolumesRead, ChaptersRead, Rating
    // Status, and a Photo column. Users will track how many chapters they have read so they
    // know where they left off. All of such is stored in the MangaList table.
    public class MangaList
    {

        public int ID { get; set; }

        [Required]
        public int MangaID { get; set; }
        
        public int VolumesRead { get; set; }

        public int ChaptersRead { get; set; }
        
        public int Rating { get; set; }

        public String Status { get; set; }

        public String Photo { get; set; }

        //Parent
        public Manga Mangas { get; set; }
    }
}
