using alatech.Contexts;
using alatech.Domains;
using alatech.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace alatech.Repositories
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de processor dentro da aplicação
    /// </summary>
    public class ProcessadorRepository : IProcessadorRepository
    {
        /// <summary>
        /// Context responsavel pelas conexoes com o BD
        /// </summary>
        private alatechContext Ctx = new alatechContext();

        /// <summary>
        /// Lista todos os processadores
        /// </summary>
        /// <returns>Lista de processadores</returns>
        public List<Processor> GetProcessadores()
        {
            return Ctx.Processors.ToList();
        }

        /// <summary>
        /// Pesquisa um ou mais processadores por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Processador(es) encontrado(s)</returns>
        public List<Processor> SearchProcessadores(string parametroBusca)
        {
            return Ctx.Processors.ToList().FindAll(p => p.Name.Contains(parametroBusca));
        }
    }
}
