using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de motherboard dentro da aplicação
    /// </summary>
    public interface IPlacaMaeRepository
    {
        /// <summary>
        /// Lista todas as placas mae
        /// </summary>
        /// <returns>Lista de placas mae</returns>
        List<Motherboard> GetPlacasMae();

        /// <summary>
        /// Pesquisa uma ou mais placas mae por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Placa(s) mae encontrada(s)</returns>
        List<Motherboard> SearchPlacasMae(string parametroBusca);
    }
}
