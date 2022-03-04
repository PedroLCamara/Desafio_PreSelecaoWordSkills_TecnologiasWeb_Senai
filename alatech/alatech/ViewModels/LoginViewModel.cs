using System.ComponentModel.DataAnnotations;

namespace alatech.ViewModels
{
    /// <summary>
    /// Classe para a definição do objeto JSON (usuario) utilizado no Login
    /// </summary>
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Senha inválida")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Nome de usuário inválido")]
        public string NomeDeUsuario { get; set; }
    }
}
