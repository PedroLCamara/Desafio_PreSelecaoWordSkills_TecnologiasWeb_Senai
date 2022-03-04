using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de graphiccard dentro da aplicação
    /// </summary>
    public interface IPlacaVideoRepository
    {
        /// <summary>
        /// Lista todas as placas de video
        /// </summary>
        /// <returns>Lista de placas de video</returns>
        List<Graphiccard> GetPlacasVideo();

        /// <summary>
        /// Pesquisa uma ou mais placas de video por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Placa(s) de video encontrada(s)</returns>
        List<Graphiccard> SearchPlacasVideo(string parametroBusca);
    }
}
