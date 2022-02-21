using System.ComponentModel.DataAnnotations;

namespace alatech.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Senha inválida")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Nome de usuário inválido")]
        public string NomeDeUsuario { get; set; }
    }
}
