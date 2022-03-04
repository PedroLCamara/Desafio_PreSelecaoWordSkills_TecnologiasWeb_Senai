using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de PowerSupply dentro da aplicação
    /// </summary>
    public class FonteRepository : IFonteRepository
    {
        /// <summary>
        /// Context responsavel pelas conexoes com o BD
        /// </summary>
        private alatechContext Ctx = new alatechContext();

        /// <summary>
        /// Lista todas as fontes
        /// </summary>
        /// <returns>Lista de fontes</returns>
        public List<Powersupply> GetFontes()
        {
            return Ctx.Powersupplies.ToList();
        }

        /// <summary>
        /// Pesquisa uma ou mais fontes por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Fonte(s) encontrada(s)</returns>
        public List<Powersupply> SearchFontes(string parametroBusca)
        {
            return Ctx.Powersupplies.ToList().FindAll(ps => ps.Name.Contains(parametroBusca));
        }
    }
}