using alatech.Domains;

namespace alatech.Interfaces
{
    public interface IUsuarioRepository
    {
        User Login(string username, string senha);

        bool Logout(string token);

        void SalvarToken(string token, User UsuarioNovoToken);

        void CriptografarSenha(User _usuario);
    }
}
