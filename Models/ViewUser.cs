using System.ComponentModel.DataAnnotations;

namespace ProfessionalNetwork.Models
{
    public class ViewUser
    {
        [Required(ErrorMessage = "Field is required")]
        [RegularExpression(@"[a-zA-Z \-]+", ErrorMessage = "Field must contain only alphabetical characters")]
        [Display(Name = "Name: ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email: ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [MinLength(8, ErrorMessage = "Must be at least 8 characters long")]
        [DataType(DataType.Password)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        [Display(Name = "Confirm: ")]
        public string PasswordConf { get; set; }
        [Required(ErrorMessage = "Field is required")]
        [Display(Name = "Description: ")]
        public string Description { get; set; }
    }
}