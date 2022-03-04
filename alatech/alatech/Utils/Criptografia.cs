namespace alatech.Utils
{
    /// <summary>
    /// Classe utilizada para definir os métodos que envolvem a criptografia de uma senha
    /// </summary>
    public static class Criptografia
    {
        /// <summary>
        /// Gera a senha criptografada (com BCrypt)
        /// </summary>
        /// <param name="Senha">Senha do formulario, fornecida pelo usuario</param>
        /// <returns>Senha criptografada</returns>
        public static string GerarHash(string Senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(Senha);
        }

        /// <summary>
        /// Compara uma senha normal com uma senha criptografada proveniente do Banco de Dados
        /// </summary>
        /// <param name="SenhaForm">Senha da requisicao (string comum)</param>
        /// <param name="SenhaBanco">Senha criptografada</param>
        /// <returns>True (Senhas compatíveis) ou false (senhas incompatíveis)</returns>
        public static bool Comparar(string SenhaForm, string SenhaBanco)
        {
            return BCrypt.Net.BCrypt.Verify(SenhaForm, SenhaBanco); ;
        }

        /// <summary>
        /// Valida a criptografia de uma string (senha)
        /// </summary>
        /// <param name="SenhaBanco">Senha proveniente do Banco de Dados</param>
        /// <returns>True (criptografada) ou false (nao criptografada)</returns>
        public static bool ValidarCriptografia(string SenhaBanco)
        {
            if (SenhaBanco.Length >= 32 && SenhaBanco.Substring(0, 1) == "$")
            {
                return true;
            }
            else return false;
        }
    }
}
