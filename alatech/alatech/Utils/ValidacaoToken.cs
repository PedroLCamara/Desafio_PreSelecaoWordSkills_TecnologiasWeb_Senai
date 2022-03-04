using alatech.Contexts;
using alatech.Domains;
using System.Linq;

namespace alatech.Utils
{
    public static class ValidacaoToken
    {
        private static alatechContext Ctx = new alatechContext();

        /// <summary>
        /// Valida a existência do token no Banco de Dados
        /// </summary>
        /// <param name="token">Token a ser validado</param>
        /// <returns>True (valido) ou false (invalido)</returns>
        public static bool ValidarToken(string token)
        {
            User UsuarioConsulta = Ctx.Users.FirstOrDefault(u => "Bearer " + u.AccessToken == token);
            if (UsuarioConsulta != null)
            {
                return true;
            }
            else return false;
        }
    }
}
