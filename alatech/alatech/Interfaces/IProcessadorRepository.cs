using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de processor dentro da aplicação
    /// </summary>
    public interface IProcessadorRepository
    {
        /// <summary>
        /// Lista todos os processadores
        /// </summary>
        /// <returns>Lista de processadores</returns>
        List<Processor> GetProcessadores();

        /// <summary>
        /// Pesquisa um ou mais processadores por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Processador(es) encontrado(s)</returns>
        List<Processor> SearchProcessadores(string parametroBusca);
    }
}
