using System.ComponentModel.DataAnnotations;
namespace api_app.Model
{

    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "O campo deve ser informado")]
        [EmailAddress(ErrorMessage = "O E-mail deve ser informado aqui")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo deve ser informado")]
        [StringLength(100,ErrorMessage ="Precisa estar entre {2} e {1} caractere")]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="As Senhas não são iguais !!!")]
        public string ConfirmPassword { get; set; }

    }
    public class LoginUserViewModel
    {
        [Required(ErrorMessage = "O campo deve ser informado")]
        [EmailAddress(ErrorMessage = "O E-mail deve ser informado aqui")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo deve ser informado")]
        [StringLength(100, ErrorMessage = "Precisa estar entre {2} e {1} caractere")]
        public string Password { get; set; }
    }
}

