using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de PowerSupply dentro da aplicação
    /// </summary>
    public interface IFonteRepository
    {
        /// <summary>
        /// Lista todas as fontes
        /// </summary>
        /// <returns>Lista de fontes</returns>
        List<Powersupply> GetFontes();

        /// <summary>
        /// Pesquisa uma ou mais fontes por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Fonte(s) encontrada(s)</returns>
        List<Powersupply> SearchFontes(string parametroBusca);
    }
}