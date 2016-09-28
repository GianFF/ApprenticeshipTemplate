using System.ComponentModel.DataAnnotations;
using TusLibros.app;
using TusLibros.app.environment;

namespace TusLibrosWeb.Models
{
    public class LoginViewModel
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