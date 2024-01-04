using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRex.Models
{
	public class Account
	{
        [Key]
        [Required(ErrorMessage = "Ban phai nhap vao User Name!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Ban phai nhap vao Password!")]
        public string Password { get; set; }
    }
}

