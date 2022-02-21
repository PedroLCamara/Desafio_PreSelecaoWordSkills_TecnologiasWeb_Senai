using alatech.Contexts;
using alatech.Domains;
using System.Linq;

namespace alatech.Utils
{
    public static class ValidacaoToken
    {
        private static alatechContext Ctx = new alatechContext();

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
