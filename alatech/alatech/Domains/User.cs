using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace alatech.Domains
{
    public partial class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome de usuário inválido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Senha inválida")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Token inválido")]
        public string AccessToken { get; set; }
    }
}
