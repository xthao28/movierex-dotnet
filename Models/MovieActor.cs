using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRex.Models
{
	public class MovieActor
	{
        [Key]
        public int ID { get; set; }

        [Required]
        public string MovieID { get; set; }
        [ForeignKey("MovieID")]
        public Movie Movie { get; set; }

        [Required]
        public int ActorID { get; set; }
        [ForeignKey("ActorID")]
        public Actor Actor { get; set; }
    }
}

