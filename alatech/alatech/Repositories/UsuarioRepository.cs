using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using alatech.Utils;
using System.Linq;

namespace alatech.Repositories
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de user dentro da aplicação
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        /// <summary>
        /// Context responsavel pelas conexoes com o BD
        /// </summary>
        private alatechContext Ctx = new alatechContext();

        /// <summary>
        /// Login com base em um nome de usuario e uma senha
        /// </summary>
        /// <param name="username">Nome de usuario de um formulario</param>
        /// <param name="senha">Senha de um formulario</param>
        /// <returns>O usuario encontrado</returns>
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

        /// <summary>
        /// Remove um token do Banco de Dados
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>True (removido) ou false (inexistente)</returns>
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

        /// <summary>
        /// Salva um token no Banco de Dados
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="UsuarioNovoToken">Usuario a receber o token</param>
        public async void SalvarToken(string token, User UsuarioNovoToken)
        {
            UsuarioNovoToken.AccessToken = token;
            Ctx.Users.Update(UsuarioNovoToken);
            await Ctx.SaveChangesAsync();
        }

        /// <summary>
        /// Criptografa uma senha
        /// </summary>
        /// <param name="_usuario">Usuario a ter a senha criptografada</param>
        public async void CriptografarSenha(User _usuario)
        {
            _usuario.Password = Criptografia.GerarHash(_usuario.Password);
            Ctx.Users.Update(_usuario);
            await Ctx.SaveChangesAsync();
        }
    }
}
