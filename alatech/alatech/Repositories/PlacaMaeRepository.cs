using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de motherboard dentro da aplicação
    /// </summary>
    public class PlacaMaeRepository : IPlacaMaeRepository
    {
        /// <summary>
        /// Context responsavel pelas conexoes com o BD
        /// </summary>
        private alatechContext Ctx = new alatechContext();

        /// <summary>
        /// Lista todas as placas mae
        /// </summary>
        /// <returns>Lista de placas mae</returns>
        public List<Motherboard> GetPlacasMae()
        {
            return Ctx.Motherboards.ToList();
        }

        /// <summary>
        /// Pesquisa uma ou mais placas mae por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Placa(s) mae encontrada(s)</returns>
        public List<Motherboard> SearchPlacasMae(string parametroBusca)
        {
            return Ctx.Motherboards.ToList().FindAll(m => m.Name.Contains(parametroBusca));
        }
    }
}