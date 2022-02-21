using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using alatech.Utils;
using System.Linq;

namespace alatech.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private alatechContext Ctx = new alatechContext();
        public User Login(string username, string senha)
        {
            User UsuarioLogin = Ctx.Users.FirstOrDefault(u => u.Username == username);
            if (UsuarioLogin != null)
            {
                if(Criptografia.ValidarCriptografia(UsuarioLogin.Password) == true)
                {
                    bool Confere = Criptografia.Comparar(senha, UsuarioLogin.Password);
                    if (Confere)
                        return UsuarioLogin;
                }
                else
                {
                    CriptografarSenha(UsuarioLogin);
                    bool Confere = Criptografia.Comparar(senha, UsuarioLogin.Password);
                    if (Confere)
                        return UsuarioLogin;
                }
            }
            return null;
        }

        public bool Logout(string token)
        {
            User UsuarioLogout = Ctx.Users.FirstOrDefault(u => "Bearer " + u.AccessToken == token);
            if (UsuarioLogout != null)
            {
                UsuarioLogout.AccessToken = "Não especificado";
                Ctx.Users.Update(UsuarioLogout);
                Ctx.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async void SalvarToken(string token, User UsuarioNovoToken)
        {
            UsuarioNovoToken.AccessToken = token;
            Ctx.Users.Update(UsuarioNovoToken);
            await Ctx.SaveChangesAsync();
        }

        public async void CriptografarSenha(User _usuario)
        {
            _usuario.Password = Criptografia.GerarHash(_usuario.Password);
            Ctx.Users.Update(_usuario);
            await Ctx.SaveChangesAsync();
        }
    }
}
