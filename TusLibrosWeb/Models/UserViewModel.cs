using System.ComponentModel.DataAnnotations;
using TusLibros.model.entities;

namespace TusLibrosWeb.Models
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}