using System;
using System.ComponentModel.DataAnnotations;

namespace MovieRex.Models
{
	public class Movie
	{
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phai nhap ma phim")]
        public string MovieID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phai nhap ten phim")]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phai nhap mo ta ve phim")]
        public string OverView { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phai nhap duong dan anh")]
        public string Backdrop_Path { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Phai nhap ngay xuat ban")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Release_Date { get; set; }

        [Required]
        public string Vote { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phai nhap ten quoc gia")]
        public string Country { get; set; }

        [Required]
        public int Limit_Age { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phai nhap thoi luong phim")]
        public string Duration { get; set; }        


    }
}

