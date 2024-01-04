using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRex.Models
{
	public class MovieGenre
	{
        [Key]
        public int ID { get; set; }

        [Required]
        public string MovieID { get; set; }
        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        [Required]
        public int GenreID { get; set; }
        [ForeignKey("GenreID")]
        public Genre Genre { get; set; }
    }
}

