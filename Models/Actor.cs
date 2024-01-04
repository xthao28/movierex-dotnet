using System;
using System.ComponentModel.DataAnnotations;
namespace MovieRex.Models
{
	public class Actor
	{
		[Key]
		public int ActorID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Phai nhap ten dien vien")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phai chon gioi tinh")]
        public bool Gender { get; set; }
        
    }
}

