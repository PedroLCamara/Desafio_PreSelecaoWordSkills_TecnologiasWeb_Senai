using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de Rammemory dentro da aplicação
    /// </summary>
    public class MemoriaRamRepository : IMemoriaRamRepository
    {
        /// <summary>
        /// Context responsavel pelas conexoes com o BD
        /// </summary>
        private alatechContext Ctx = new alatechContext();

        /// <summary>
        /// Lista todas as memorias RAM
        /// </summary>
        /// <returns>Lista de memorias RAM</returns>
        public List<Rammemory> GetMemoriasRam()
        {
            return Ctx.Rammemories.ToList();
        }

        /// <summary>
        /// Pesquisa uma ou mais memorias RAM por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Memoria(s) RAM encontrada(s)</returns>
        public List<Rammemory> SearchMemoriasRam(string parametroBusca)
        {
            return Ctx.Rammemories.ToList().FindAll(rm => rm.Name.Contains(parametroBusca));
        }
    }
}
