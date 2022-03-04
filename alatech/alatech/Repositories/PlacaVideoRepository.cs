using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de graphiccard dentro da aplicação
    /// </summary>
    public class PlacaVideoRepository : IPlacaVideoRepository
    {
        /// <summary>
        /// Context responsavel pelas conexoes com o BD
        /// </summary>
        private alatechContext Ctx = new alatechContext();

        /// <summary>
        /// Lista todas as placas de video
        /// </summary>
        /// <returns>Lista de placas de video</returns>
        public List<Graphiccard> GetPlacasVideo()
        {
            return Ctx.Graphiccards.ToList();
        }

        /// <summary>
        /// Pesquisa uma ou mais placas de video por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Placa(s) de video encontrada(s)</returns>
        public List<Graphiccard> SearchPlacasVideo(string parametroBusca)
        {
            return Ctx.Graphiccards.ToList().FindAll(gc => gc.Name.Contains(parametroBusca));
        }
    }
}