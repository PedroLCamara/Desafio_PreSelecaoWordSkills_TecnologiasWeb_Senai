using alatech.Domains;

namespace alatech.Interfaces
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de user dentro da aplicação
    /// </summary>
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Login com base em um nome de usuario e uma senha
        /// </summary>
        /// <param name="username">Nome de usuario de um formulario</param>
        /// <param name="senha">Senha de um formulario</param>
        /// <returns>O usuario encontrado</returns>
        User Login(string username, string senha);

        /// <summary>
        /// Remove um token do Banco de Dados
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>True (removido) ou false (inexistente)</returns>
        bool Logout(string token);

        /// <summary>
        /// Salva um token no Banco de Dados
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="UsuarioNovoToken">Usuario a receber o token</param>
        void SalvarToken(string token, User UsuarioNovoToken);

        /// <summary>
        /// Criptografa uma senha
        /// </summary>
        /// <param name="_usuario">Usuario a ter a senha criptografada</param>
        void CriptografarSenha(User _usuario);
    }
}
