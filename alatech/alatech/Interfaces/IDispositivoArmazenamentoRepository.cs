using alatech.Domains;
using System.Collections.Generic;

namespace alatech.Interfaces
{
    /// <summary>
    /// Define as ações CRUD que seram realizadas com a entidade de StorageDevice dentro da aplicação
    /// </summary>
    public interface IDispositivoArmazenamentoRepository
    {
        /// <summary>
        /// Lista todos os dispositivos de armazenamento
        /// </summary>
        /// <returns>Lista de dispositivos de armazenamento</returns>
        List<Storagedevice> GetDispositivosArmazenamento();

        /// <summary>
        /// Pesquisa um ou mais dispositivo de armazenamento por uma query de busca
        /// </summary>
        /// <param name="parametroBusca">Query de busca</param>
        /// <returns>Dispositivo(s) encontrado(s)</returns>
        List<Storagedevice> SearchDispositivosArmazenamento(string parametroBusca);
    }
}
