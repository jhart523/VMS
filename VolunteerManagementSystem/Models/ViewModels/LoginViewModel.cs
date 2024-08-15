using System.ComponentModel.DataAnnotations;

namespace VolunteerManagementSystem.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Enter your username")]
        public string UserName { get; set; }

        
        [Required]
        
        public string Password { get; set; }    


    }
}
