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

        private static DevelopmentEnvironment Environment = new DevelopmentEnvironment(new PersitentDataBaseStrategy());
        private static IYourBooksApplication Application = Environment.GetApplication();

        public void Log()
        {
            Application.RegisterClient("qwe", "123");

        }
    }
}