using System;
using System.ComponentModel.DataAnnotations;

namespace MovieRex.Models
{
	public class Genre
	{
        [Key]
        public int GenreID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phai nhap ten the loai phim")]
        public string GenreTitle { get; set; }
        
    }
}

