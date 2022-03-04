using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de Rammemory dentro da aplicação
    /// </summary>
    public interface IMemoriaRamRepository
    {
        /// <summary>
        /// Lista todas as memorias RAM
        /// </summary>
        /// <returns>Lista de memorias RAM</returns>
        List<Rammemory> GetMemoriasRam();

        /// <summary>
        /// Pesquisa uma ou mais memorias RAM por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Memoria(s) RAM encontrada(s)</returns>
        List<Rammemory> SearchMemoriasRam(string parametroBusca);
    }
}
