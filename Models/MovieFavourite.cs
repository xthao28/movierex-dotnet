using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRex.Models
{
	public class MovieFavourite
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string MovieID { get; set; }
        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        [Required]
        public string UserName { get; set; }
        [ForeignKey("UserName")]
        public Account Account { get; set; }
        
    }
}

