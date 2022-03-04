using alatech.Domains;
using alatech.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace alatech.Interfaces
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de user dentro da aplicação
    /// </summary>
    public interface IMaquinaRepository
    {
        /// <summary>
        /// Lista todas as maquinas
        /// </summary>
        /// <returns>Uma lista de maquinas</returns>
        List<Machine> GetMaquinas();

        /// <summary>
        /// Pesquisa uma ou mais maquinas por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Maquina(s) encontrada(s)</returns>
        List<Machine> SearchMaquinas(string parametroBusca);

        /// <summary>
        /// Cadastra uma maquina
        /// </summary>
        /// <param name="maquina">Maquina a ser cadastrada</param>
        /// <returns>Maquina cadastrada</returns>
        Machine PostMaquina(Machine maquina);

        /// <summary>
        /// Retorna uma maquina com base em um objeto CadastroMaquinaViewModel
        /// </summary>
        /// <param name="maquina">Objeto</param>
        /// <returns>Retorno (objeto Machine)</returns>
        Machine RetornarMaquina(CadastroMaquinaViewModel maquina);

        /// <summary>
        /// Retorna uma maquina com base em um objeto VerificarIncompatibilidadeViewModel
        /// </summary>
        /// <param name="maquina">Objeto</param>
        /// <returns>Retorno (objeto Machine)</returns>
        Machine RetornarMaquina(VerificarIncompatibilidadeViewModel maquina);

        /// <summary>
        /// Atualiza uma maquina
        /// </summary>
        /// <param name="maquina">Maquina a ser atualizada</param>
        /// <returns>Maquina atualizada</returns>
        Machine PutMaquina(Machine maquina);

        /// <summary>
        /// Pesquisa uma maquina pelo seu id
        /// </summary>
        /// <param name="id">Id da busca</param>
        /// <returns>Maquina encontrada</returns>
        Machine GetByIdMaquina(int id);

        /// <summary>
        /// Deleta uma maquina
        /// </summary>
        /// <param name="id">Id da maquina a ser deletada</param>
        void DeleteMaquina(int id);
    }
}